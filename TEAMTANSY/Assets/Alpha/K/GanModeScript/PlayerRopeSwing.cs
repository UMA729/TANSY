using System.Collections;
using UnityEngine;

public class PlayerRopeSwing : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public DistanceJoint2D distanceJoint;
    public Transform player;
    public Transform handPosition;
    public LayerMask ceilingLayer;       // �h����V��̃��C���[
    public float maxRopeLength = 15f;    // �L�т���܂ł̒���
    public float ropeExtendSpeed = 5f;   // ���[�v���ł鑬��
    public float launchAngle = 45f;      // �ˏo�p�x
    public float ropeShortenSpeed = 10f; // ���[�v���k�߂�ck�x
    public float shortenRange = 6.0f;    // ���[�v���k�߂钷��
    public float Movingshorten = 4.0f;
    public bool isSwinging;              // �X�E�B���O�����ۂ�
    private bool isMoving;
    public Rigidbody2D pendulumRigidbody;// �U��q���s���I�u�W�F�N�g��rb

    private Vector2 ropeDirection;       // ���[�v�̋���
    private Vector2 ropeAnchor;          // �ڑ��_
    private Transform movingPlatform;    // �����u���b�N��Transform
    private Vector2 platformOffset;      // �����u���b�N�Ƃ̑��Έʒu

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
        if (lineRenderer.enabled && isSwinging)
        {
            if (movingPlatform != null)
            {
                // �����u���b�N�ɐڑ����Ă���ꍇ�A�ڑ��_���X�V
                ropeAnchor = (Vector2)movingPlatform.position + platformOffset;
                distanceJoint.connectedAnchor = ropeAnchor;
            }
            //// ���[�v�̕`��X�V
            //lineRenderer.SetPosition(0, handPosition.position);
            Vector2 endPosition = (Vector2)handPosition.position + (ropeAnchor - (Vector2)handPosition.position).normalized * distanceJoint.distance;
            lineRenderer.SetPosition(1, endPosition);

            if (distanceJoint.distance > shortenRange)
            {
                distanceJoint.distance -= ropeShortenSpeed * Time.deltaTime;
            }
        }

        lineRenderer.SetPosition(0, handPosition.position);
    }

    public void ExtendRope()
    {
        float angleInRadians = launchAngle * Mathf.Deg2Rad;

        //���[�v�̊p�x���X�V
        ropeDirection = new Vector2(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians)).normalized;
        //���[�v���L�т���ʒu��ݒ�
        Vector3 ropeEndPoint = handPosition.position + (Vector3)ropeDirection * maxRopeLength;
        //linerenderer�𖳌���
        lineRenderer.enabled = true;
        StartCoroutine(AnimateRopeExtension(ropeEndPoint));
    }

    private IEnumerator AnimateRopeExtension(Vector3 targetPoint)
    {
        float distanceCovered = 0f;

        //���[�v���L�т���܂ŉ�
        while (distanceCovered < maxRopeLength)
        {
            distanceCovered += ropeExtendSpeed * Time.deltaTime; //���[�v���L�т���܂ł̎��Ԃ��X�V
            float interpolatedDistance = Mathf.SmoothStep(0, maxRopeLength, distanceCovered / maxRopeLength);

            Vector3 nextPosition = (Vector2)handPosition.position + ropeDirection * interpolatedDistance;
            lineRenderer.SetPosition(1, nextPosition);

            RaycastHit2D hit = Physics2D.Raycast(handPosition.position, ropeDirection, interpolatedDistance, ceilingLayer);

            Debug.DrawLine(handPosition.position, handPosition.position + (Vector3)ropeDirection * maxRopeLength, Color.red);

            if (hit.collider != null)
            {
                Debug.Log($"Hit point: {hit.point}");
                StartSwing(hit.collider.transform, hit.point);
                yield break;
            }

            yield return null;
        }
        //�L�т���ƃ��[�v��߂�
        ReleaseRope();
    }

    private void StartSwing(Transform hitTransform, Vector2 hitPoint)
    {
        Collider2D hitcollider = Physics2D.OverlapPoint(hitPoint, ceilingLayer);

        if (hitcollider != null)//Movebrock�Ƃ����^�O�Ƀq�b�g���������
        {
            Debug.Log("collider�����m���܂����B");
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
        else if(hitcollider == null)
        {

            Debug.Log("nohit");
        }

        ropeAnchor = hitPoint;                                                        //�ڑ��_���X�V
        distanceJoint.connectedAnchor = ropeAnchor;                                   //�ڑ��_�����ɐU��q������s��
        distanceJoint.distance = Vector2.Distance(handPosition.position, ropeAnchor); //���[�v���h�������ʒu�ƃv���C���[�Ƃ̋���
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
