using System.Collections;
using UnityEngine;

public class PlayerRopeSwing : MonoBehaviour
{
    //�萔
    public float maxRopeLength = 15f;      // �L�т���܂ł̒���
    public float ropeExtendSpeed = 5f;     // �t�b�N���ł鑬��
    public float launchAngle = 45f;        // �ˏo�p�x
    public float ropeShortenSpeed = 10f;   // �t�b�N���k�߂鑬�x
    public float shortenRange = 6.0f;      // �t�b�N���k�߂钷��
    public float Movingshorten = 4.0f;     // �����u���b�N�Ƀt�b�N�����Ă��鎞�ɒZ�����鑬�x

    //�R���|�[�l���g
    public LineRenderer lineRenderer;      //�t�b�N��`�悷�邽�߂ɕK�v
    public DistanceJoint2D distanceJoint;  //�U��q����ɕK�v
    public AudioClip RopeSound;            //�t�b�N���o����
    public Transform player;               //��l���̃I�u�W�F�N�g���W
    public Transform handPosition;         //��l���̎���W
    public Rigidbody2D pendulumRigidbody;  // �U��q���s���I�u�W�F�N�g��Rb

    public LayerMask ceilingLayer;         // �h����V��̃��C���[

    //�t���O
    public bool isSwinging;                // �X�E�B���O�t���O
    private bool isMoving;                 // �I�u�W�F�N�g�������Ă���t���O
   
    //�t�b�N���W�Ȃ�
    private Vector2 ropeDirection;         // �t�b�N�̋���
    private Vector2 ropeAnchor;            // �ڑ��_
    private Transform movingPlatform;      // �����u���b�N��Transform
    private Vector2 platformOffset;        // �����u���b�N�Ƃ̑��Έʒu

    void Start()
    {
        lineRenderer.enabled  = false;   //----------------------
        distanceJoint.enabled = false;   //
        isSwinging            = false;   // ������
        isMoving              = false;   //
        movingPlatform        =  null;   //----------------------
    }

    void Update()
    {
        //�X�E�B���O�����Ă��Ȃ��
        if (lineRenderer.enabled && isSwinging)
        {
            //�����u���b�N����
            if (movingPlatform != null)
            {
                // �����u���b�N�ɐڑ����Ă���ꍇ�A�ڑ��_���X�V
                ropeAnchor = (Vector2)movingPlatform.position + platformOffset;
                distanceJoint.connectedAnchor = ropeAnchor;
            }
            // �t�b�N�̕`��X�V
            Vector2 endPosition = (Vector2)handPosition.position + (ropeAnchor - (Vector2)handPosition.position).normalized * distanceJoint.distance;
            lineRenderer.SetPosition(1, endPosition);

            //�t�b�N�����܂ŒZ������
            if (distanceJoint.distance > shortenRange)
            {
                distanceJoint.distance -= ropeShortenSpeed * Time.deltaTime;
            }
        }
        //�t�b�N���o��ʒu���X�V
        lineRenderer.SetPosition(0, handPosition.position);
    }

    public void ExtendRope()
    {
        float angleInRadians = launchAngle * Mathf.Deg2Rad;

        //�t�b�N�̐��K��
        ropeDirection = new Vector2(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians)).normalized;
        //�t�b�N���L�т���ʒu��ݒ�
        Vector3 ropeEndPoint = handPosition.position + (Vector3)ropeDirection * maxRopeLength;
        //linerenderer�𖳌���
        lineRenderer.enabled = true;
        StartCoroutine(AnimateRopeExtension(ropeEndPoint));
    }

    private IEnumerator AnimateRopeExtension(Vector3 targetPoint)
    {
        AudioSource.PlayClipAtPoint(RopeSound,handPosition.position); //���[�v���ˉ�

        float distanceCovered = 0f;

        //�t�b�N���L�т���܂ŉ�
        while (distanceCovered < maxRopeLength)
        {
            //�t�b�N���L�т鑬��
            distanceCovered += ropeExtendSpeed * Time.deltaTime; 

            //�L�ѓr���̃t�b�N�̒�������
            float interpolatedDistance = Mathf.SmoothStep(0, maxRopeLength, distanceCovered / maxRopeLength);

            //�t�b�N�����݂ǂꂭ�炢�L�тĂ��邩
            Vector3 nextPosition = (Vector2)handPosition.position + ropeDirection * interpolatedDistance;

            //���݂̃t�b�N�̒�������`��
            lineRenderer.SetPosition(1, nextPosition);

            //�t�b�N�������C���[��T�����C�L���X�g
            RaycastHit2D hit = Physics2D.Raycast(handPosition.position, ropeDirection, interpolatedDistance, ceilingLayer);

            //�t�b�N���o�Ă��邩�̃f�o�b�O
            Debug.DrawLine(handPosition.position, handPosition.position + (Vector3)ropeDirection * maxRopeLength, Color.red);

            //�t�b�N���q�b�g������
            if (hit.collider != null)
            {
                Debug.Log($"Hit point: {hit.point}");
                //�X�E�B���O�J�n�i���������R���C�_�[�Ɠ��������ʒu���W�l��n���j
                StartSwing(hit.collider.transform, hit.point);
                yield break;
            }

            yield return null;
        }
        //�L�т���ƃt�b�N��߂�
        ReleaseRope();
    }

    private void StartSwing(Transform hitTransform, Vector2 hitPoint)
    {
        //�t�b�N��������R���C�_�[
        Collider2D hitcollider = Physics2D.OverlapPoint(hitPoint, ceilingLayer);

        //Movebrock�Ƃ����^�O�Ƀq�b�g���������
        if (hitcollider != null)
        {
            Debug.Log("collider�����m���܂����B");
            //�R���C�_�[�̃^�O��Movebrock�ł����
            if (hitcollider.CompareTag("Movebrock"))
            {
                //movingPlatform�Ƀ��[�v���h�������ʒu����
                movingPlatform = hitTransform;
                //�h����ʒu�ƃu���b�N���S���W�����Ƀu���b�N�ƃ��[�v���ǂ̂��炢����Ă���̂��̑��Έʒu
                platformOffset = hitPoint - (Vector2)movingPlatform.position;
                ropeShortenSpeed += Movingshorten;
                isMoving = true;
            }
        }
        //�I�u�W�F�N�g���Ȃ����
        else if(hitcollider == null)
        {

            Debug.Log("nohit");
        }

        ropeAnchor = hitPoint;                                                        //�ڑ��_���X�V
        distanceJoint.connectedAnchor = ropeAnchor;                                   //�ڑ��_�����ɐU��q������s��
        distanceJoint.distance = Vector2.Distance(handPosition.position, ropeAnchor); //�t�b�N���h�������ʒu�ƃv���C���[�Ƃ̋���
        distanceJoint.enabled = true;                                                 //distance�W���C���g�𖳌���
        isSwinging = true;                                                            //�X�E�B���O��
    }

    public void ReleaseRope()
    {
        if (isMoving)
        {
            ropeShortenSpeed -= Movingshorten;
            isMoving = false;
        }
        lineRenderer.enabled = false; //linerenderer��L��
        distanceJoint.enabled = false;//distancejoint��L��
        isSwinging = false;          
        movingPlatform = null; 
    }
}
