using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireget : MonoBehaviour
{
    public LayerMask interactableLayer; // �ΏۂƂȂ郌�C���[�}�X�N

    private bool isInRange = false;    // �͈͓��ɂ��邩�ǂ���
    public float duration = 5f;       // �������ԁi�b�j
    public float animeDuration = 5f;       // �������ԁi�b�j
    public float reviveSec = 0f;
    public float animeSec = 0f;
    private fireBullet FB;
    private ItemSelector IS;

    Animator animator;
    public string torchtrue  = "torch Animation";
    public string torchfalse = "torchfalse Animation";
    public string torchrevive = "torchrevive Animation";
    public bool torchCharge = false;
    public bool ColliderSerch = false;
    public GameObject torchBord;

    public string nowAnime = "";
    string oldAnime = "";

    private void Awake()
    {
        Application.targetFrameRate = 60; //
    }

    void Start()
    {
        torchBord.SetActive(false);
        animator = GetComponent<Animator>();
        FB = FindAnyObjectByType<fireBullet>();
        IS = FindObjectOfType<ItemSelector>();
        nowAnime = torchtrue;
        oldAnime = torchtrue;
    }
    void Update()
    {
        // �͈͓��ŉE�N���b�N���ꂽ�ꍇ
        if (isInRange && Input.GetMouseButtonDown(1) && FB.fireBaff == false && IS.currentIndex == 1) // 1�͉E�N���b�N
        {
            PerformRaycast();
            if (torchBord == true)
            {
                Destroy(torchBord, 1f);
            }
        }
        //�g�[�`�̃A�j���[�V����
        if (torchCharge == true)
        {
            if (nowAnime != torchrevive)
            {
                nowAnime = torchfalse;
            }

            reviveSec += Time.deltaTime;
            if (reviveSec >= duration)
            {
                nowAnime = torchrevive;
                torchCharge = false;
                reviveSec = 0f;
            }
        }
        if (nowAnime != oldAnime)
        {
            oldAnime = nowAnime;
            animator.Play(nowAnime);
            //Debug.Log($"{torchtrue}");
            //Debug.Log($"{torchfalse}");
            //Debug.Log($"{torchrevive}");
            //Debug.Log($"{nowAnime}");
        }

        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        // �A�j���[�V�������I�����Ă��邩�m�F
        if (stateInfo.IsName("torchrevive Animation") && stateInfo.normalizedTime >= 1.0f)
        {
            Debug.Log("�A�j���[�V�������I�����܂���");
            // �A�j���[�V�����I����̏���
            nowAnime = torchtrue;
        }
    }


    private void PerformRaycast()
    {
        Active();
    }

   

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) // �^�O���v���C���[�Ȃ�
        {
            Debug.Log("�͈͂ɓ���܂���");
            isInRange = true;
            ColliderSerch = true;
            Debug.Log($"isInRange��{isInRange}");
            torchBord.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) // �^�O���v���C���[�Ȃ�
        {
            Debug.Log("�͈͂���ł܂���");
            isInRange = false;
            ColliderSerch = false;
            Debug.Log($"isInRange��{isInRange}");
            torchBord.SetActive(false);
        }
    }
    public void Active()
    {
        FB.fireBaff = true;
        torchCharge = true;
    }
}