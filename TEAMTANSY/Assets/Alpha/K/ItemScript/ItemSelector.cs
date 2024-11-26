using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSelector : MonoBehaviour
{
    public Image mainItemImage;        // �����̃A�C�e���摜
    public Image previousItemImage;    // �O�̃A�C�e���摜
    public Image nextItemImage;        // ���̃A�C�e���摜
    public Image ganModeImage;�@�@�@�@�@//���񃂁[�h�̃C���[�W
    public List<Sprite> itemSpritesMagic = new List<Sprite>(); // �A�C�e���摜�̃��X�g
    public List<Sprite> itemSpritesOp = new List<Sprite>();
    public List<string> magicName;
    public List<string> optionName;
    public string selectMode;

    private int modechangeIndex;
    private PlayerRopeSwing PRS;
    private BulletComtller BCN;
    private WindBullet BCW;
    private PortalGun PG;
    private fireBullet FB;
    private MPController mp;
    private int currentIndex = 0;
    private int currentIndexMode = 0;
    private bool isGanMagic = false;
    private bool isGanOp = false;

    void Start()
    {
        ganModeImage.sprite = itemSpritesOp[modechangeIndex];

        PRS = FindObjectOfType<PlayerRopeSwing>(); // PRSClass�̃C���X�^���X���擾
        BCN = FindObjectOfType<BulletComtller>();
        BCW = FindObjectOfType<WindBullet>();
        PG = FindObjectOfType<PortalGun>();
        mp = FindObjectOfType<MPController>();
        FB = FindObjectOfType<fireBullet>();

        isGanMagic = true;         //isGanMagic�̃t���O�����낷
        isGanOp = false;            //isGanOp�̃t���O�����낷
        UpdateItemImages();
    }

    void Update()
    {
        // E�L�[�ŉE�AQ�L�[�ō��ɃA�C�e����؂�ւ���
        if (Input.GetKeyDown(KeyCode.E))
        {
            MoveNext();
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            MovePrevious();
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("W�L�[�������ꂽ�̂ŏe���[�h�̐؂�ւ��Ɉڂ�܂�");
            GanModeChoose();
        }
        else if(Input.GetMouseButtonDown(0) && player.gameState != "playerover")
        {
            UseSelectedItem();
        }
    }

    void MoveNext()
    {
        if (isGanMagic)
        {
            currentIndex = (currentIndex + 1) % itemSpritesMagic.Count;
        }
        else if (isGanOp)
        {
            currentIndex = (currentIndex + 1) % itemSpritesOp.Count;
        }
        UpdateItemImages();
    }

    void MovePrevious()
    {
        if (isGanMagic)
        {
            currentIndex = (currentIndex - 1 + itemSpritesMagic.Count) % itemSpritesMagic.Count;
        }
        else if (isGanOp)
        {
            currentIndex = (currentIndex - 1 + itemSpritesOp.Count) % itemSpritesOp.Count;
        }
        UpdateItemImages();
    }

    void UpdateItemImages()
    {
        Debug.Log(currentIndex);
        if (isGanMagic)
        {
            // �����̃A�C�e�����X�V
            mainItemImage.sprite = itemSpritesMagic[currentIndex];

            // �O�̃A�C�e���̃C���f�b�N�X���v�Z���čX�V
            int previousIndex = (currentIndex - 1 + itemSpritesMagic.Count) % itemSpritesMagic.Count;
            previousItemImage.sprite = itemSpritesMagic[previousIndex];

            // ���̃A�C�e���̃C���f�b�N�X���v�Z���čX�V
            int nextIndex = (currentIndex + 1) % itemSpritesMagic.Count;
            nextItemImage.sprite = itemSpritesMagic[nextIndex];
        }
        else if(isGanOp)
        {
            // �����̃A�C�e�����X�V
            mainItemImage.sprite = itemSpritesOp[currentIndex];

            // �O�̃A�C�e���̃C���f�b�N�X���v�Z���čX�V
            int previousIndex = (currentIndex - 1 + itemSpritesOp.Count) % itemSpritesOp.Count;
            previousItemImage.sprite = itemSpritesOp[previousIndex];

            // ���̃A�C�e���̃C���f�b�N�X���v�Z���čX�V
            int nextIndex = (currentIndex + 1) % itemSpritesOp.Count;
            nextItemImage.sprite = itemSpritesOp[nextIndex];
        }
    }
    void GanModeChoose()
    {
        if (isGanOp)
        {
            currentIndexMode = (currentIndexMode) % itemSpritesOp.Count;

            Debug.Log("���@�e�ɐ؂�ւ��܂�");
            Debug.Log(currentIndexMode);
            modechangeIndex = currentIndexMode % itemSpritesOp.Count;
            Debug.Log(modechangeIndex);
            ganModeImage.sprite = itemSpritesOp[modechangeIndex];

            isGanOp    = false;
            isGanMagic = true;
        }
        else if (isGanMagic)
        {
            currentIndexMode = (currentIndexMode) % itemSpritesMagic.Count;

            Debug.Log("�e�@�\�ɐ؂�ւ��܂�");

            modechangeIndex = currentIndexMode % itemSpritesMagic.Count;
            Debug.Log(modechangeIndex);
            ganModeImage.sprite = itemSpritesMagic[modechangeIndex];

            isGanMagic = false;
            isGanOp    = true;
        }
        currentIndex = 0;
        UpdateItemImages();
    }
    public void UseSelectedItem()
    {
        Debug.Log(currentIndex);

        if (isGanMagic)
        {
            selectMode = magicName[currentIndex];
            switch (selectMode)
            {
                case "�ʒe��":
                    Debug.Log("�ʏ�e�ۂ��I�΂�܂����B");
                    if (Time.time >= BCN.nextFireTime)
                    {
                        BCN.LaunchBall();
                        BCN.nextFireTime = Time.time + 1f / BCN.fireRate; // �N�[���^�C����ݒ�


                        //+++ �T�E���h�Đ��ǉ� +++
                        //�T�E���h�Đ�
                        AudioSource soundPlayer = GetComponent<AudioSource>();
                        if (soundPlayer != null)
                        {
                            //BGM��~
                            soundPlayer.Stop();
                            soundPlayer.PlayOneShot(BCN.meShoot);
                        }
                    }
                    break;
                case "�e�ە�":
                    Debug.Log("�����I������܂���");
                    if (mp.Mp >= 0 && Time.time >= BCW.nextFireTime) 
                    {
                        BCW.LaunchBall();
                        BCW.nextFireTime = Time.time + 5f / BCW.fireRate; // �N�[���^�C����ݒ�


                        //+++ �T�E���h�Đ��ǉ� +++
                        //�T�E���h�Đ�
                        AudioSource soundPlayer = GetComponent<AudioSource>();
                        if (soundPlayer != null)
                        {
                            //BGM��~
                            soundPlayer.Stop();
                            soundPlayer.PlayOneShot(BCW.Bullet);
                        }
                    }
                    break;
                case "�e�ۉ�":
                    Debug.Log("�΂��I������܂����B");
                    if (mp.Mp >= 0 && Time.time >= FB.nextFireTime)
                    {
                        FB.LaunchBall();
                    }
                    break;
            }
        }

        if (isGanOp)
        {
            selectMode = optionName[currentIndex];

            switch (selectMode)
            {
                case "�O���b�v��":
                    Debug.Log("�O���b�v�����Z���N�g����܂���");
                    Debug.Log("lineRenderer�̏��: " + PRS.lineRenderer.enabled);
                    if (!PRS.lineRenderer.enabled)
                    {
                        PRS.ExtendRope();
                    }
                    break;
                case "�|�[�^��":
                    PG.CreatePortal();
                    break;
            }
        }
        if (PRS.isSwinging)
        {
            PRS.ReleaseRope();
        }
    }
    public void ObtainMagicItem(Sprite newMagicSprite, string newMagicName)
    {
        Debug.Log("�{���擾���܂���");
        // �V�����A�C�e�������X�g�ɒǉ�
        itemSpritesMagic.Add(newMagicSprite);
        magicName.Add(newMagicName);

        // ���ݑI�𒆂̃A�C�e�����V�����ǉ����ꂽ�A�C�e���ɂȂ�悤�ɐݒ�
        currentIndex = itemSpritesMagic.Count - 1;

        if (isGanOp)
        {
            isGanOp = false;
            isGanMagic = true;
            ganModeImage.sprite = itemSpritesOp[modechangeIndex]; //�e�@�\�����[�h�`�F���W�C���[�W
        }
        // UI���X�V
        UpdateItemImages();

        Debug.Log($"�V�������@�A�C�e�� '{newMagicName}' ���擾���܂����B���݂̃A�C�e����: {itemSpritesMagic.Count}");
    }
}