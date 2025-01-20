using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireget : MonoBehaviour
{
    public LayerMask interactableLayer; // �ΏۂƂȂ郌�C���[�}�X�N

    private bool isInCol = false;      // �R���C�_�[�͈͓��ɂ��邩�ǂ���
    public float duration = 5f;        // �����܂���������܂ł̎���(�ړI)
    public float reviveSec = 0f;       // �����܂���������܂ł̎���(�v��)
    public float animeSec = 0f;        // �A�j���[�V�������I���܂ł̎���
    private fireBullet FB;             // �X�N���v�g�ϐ�
    private ItemSelector IS;           //

    Animator animator;                                  //�����܂A�j���[�^�[�ϐ�
    public string torchtrue  = "torch Animation";       //�ʏ펞�A�j���[�V����
    public string torchfalse = "torchfalse Animation";  //�Βe�ۂŃo�t�擾���A�j���[�V����
    public string torchrevive = "torchrevive Animation";//�����A�j���[�V����
    public bool torchCharge = true;                    //�Βe�ۂł��Ă����Ԃ��ǂ����̃t���O
    public GameObject torchBord;                        //�����܂����I�u�W�F�N�g

    private string nowAnime = "";�@�@  //���݃A�j���[�V����
    private string oldAnime = "";      //�O�̃A�j���[�V����

    private void Awake()
    {
        Application.targetFrameRate = 60; //�t���[�����[�g��60fps�ɐ���
    }

    void Start()
    {
        torchBord.SetActive(false);             //�����܂���obj������
        animator = GetComponent<Animator>();    //�A�j���[�^�[�擾
        FB = FindAnyObjectByType<fireBullet>(); //fireBullet�X�N���v�g�I�u�W�F�N�g�擾
        IS = FindObjectOfType<ItemSelector>();  //ItemSelector�X�N���v�g�I�u�W�F�N�g�擾
        nowAnime = torchtrue;                   
        oldAnime = torchtrue;                   
    }
    void Update()
    {
        // �͈͓��ŉE�N���b�N���ꂽ�ꍇ
        if (isInCol && Input.GetMouseButtonDown(1) && // 1�͉E�N���b�N
            FB.fireBaff == false &&                   // �΃o�t���擾��
            IS.currentIndex == 1)                     // �I��e�ۂ��Βe�ۂ̎�
        {
            FireGet();                 
            if (torchBord == true)
            {
                Destroy(torchBord, 1f); //�g�[�`�̐����I�u�W�F�N�g������
            }
        }
        //�����܂�Ԃ��o�t�擾�s�̎�
        if (torchCharge == false)
        {
            //���݃A�j���[�V�����������A�j���[�V�����ł͂Ȃ��ꍇ
            if (nowAnime != torchrevive)
            {                           
                nowAnime = torchfalse;
            }

            reviveSec += Time.deltaTime; //�����܂������ԑ���J�n
            if (reviveSec >= duration)
            {
                nowAnime = torchrevive;//�����A�j���[�V���������݃A�j���[�V������
                torchCharge = true;   //�����܂��Βe�ۂōĎ擾�\��
                reviveSec = 0f;        //�����܂������Ԃ��ēx������
            }
        }
        //���݃A�j���[�V�������ς�����ꍇ
        if (nowAnime != oldAnime)    
        {
            oldAnime = nowAnime;
            animator.Play(nowAnime); //���݂̃A�j���[�V�������Đ�
        }

        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0); //�A�j���[�V�����̌����ϐ�

        // �A�j���[�V�������I�����Ă��邩�m�F
        if (stateInfo.IsName("torchrevive Animation") && stateInfo.normalizedTime >= 1.0f)
        {
            Debug.Log("�A�j���[�V�������I�����܂���");
            // �A�j���[�V�����I����̏���
            nowAnime = torchtrue;
        }
    }

   

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) // �^�O���v���C���[�Ȃ�
        {
            isInCol = true;                                       //�R���C�_�[�̒�
            //���@�{�������Ă�����
            if(torchBord != null && ItemKeeper.hasMagicBook == 1)
            torchBord.SetActive(true);//�����܂����I�u�W�F��������悤��                           
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) // �^�O���v���C���[�Ȃ�
        {
            isInCol = false;                //�R���C�_�[�O
            //�����܂����I�u�W�F�N�g������ꍇ
            if(torchBord != null)
            torchBord.SetActive(false);//�����I�u�W�F�������Ȃ��悤��
        }
    }
    public void FireGet()
    {
        if (FB.fireBaff == false)
        {
            BaffIcon BI;                          //�X�N���v�g�ϐ�
            BI = FindObjectOfType<BaffIcon>();    //BaffIcon�X�N���v�g���擾

            BI.Set_BuffandKey_Icon(false, 1);
        }
        FB.fireBaff = true;                       //�΃o�t�擾
        torchCharge = false;                       //�����܂Ńo�t���擾���ł��Ȃ���Ԃ�
    }
}