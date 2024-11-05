using System.Collections;
using UnityEngine;

public class PlayerRopeSwing : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public DistanceJoint2D distanceJoint;
    public Transform player;
    public Transform handPosition;
    public LayerMask ceilingLayer;
    public LayerMask groundLayer; // Ground layer to detect ground collisions
    public float maxRopeLength = 15f;
    public float ropeExtendSpeed = 5f;
    public float launchAngle = 45f;
    public bool isSwinging = false;
    
    private Vector2 ropeDirection;
    private Vector2 ropeAnchor;
    void Start()
    {
        lineRenderer.enabled = false;
        distanceJoint.enabled = false;
    }

    void Update()
    {

        if (lineRenderer.enabled && isSwinging)
        {
            // ���[�v�̎n�_����̈ʒu�ɍX�V
            lineRenderer.SetPosition(0, handPosition.position);

            // LineRenderer�̒�����DistanceJoint2D�ɍ��킹��
            Vector2 endPosition = (Vector2)handPosition.position + (ropeAnchor - (Vector2)handPosition.position).normalized * distanceJoint.distance;
            lineRenderer.SetPosition(1, endPosition);

            // ���[�v���ő咷���𒴂��������
            if (distanceJoint.distance >= maxRopeLength)
            {
                ReleaseRope();
            }
        }

        // ���N���b�N�Ń��[�v����
        
    }

    //���[�v���˃��\�b�h
    public void ExtendRope()
    {
        // �Œ�p�x�Ń��[�v������ݒ�
        float angleInRadians = launchAngle * Mathf.Deg2Rad;
        ropeDirection = new Vector2(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians)).normalized;

        Vector3 ropeEndPoint = handPosition.position + (Vector3)ropeDirection * maxRopeLength;

        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, handPosition.position);
        lineRenderer.SetPosition(1, handPosition.position);

        StartCoroutine(AnimateRopeExtension(ropeEndPoint));
    }
    public IEnumerator AnimateRopeExtension(Vector3 targetPoint)
    {
        float distanceCovered = 0f;

        while (distanceCovered < maxRopeLength)
        {
            distanceCovered += ropeExtendSpeed * Time.deltaTime;
            float interpolatedDistance = Mathf.SmoothStep(0, maxRopeLength, distanceCovered / maxRopeLength);

            Vector3 nextPosition = (Vector2)handPosition.position + ropeDirection * interpolatedDistance;
            lineRenderer.SetPosition(0, handPosition.position);
            lineRenderer.SetPosition(1, nextPosition);

            RaycastHit2D hit = Physics2D.Raycast(handPosition.position, ropeDirection, interpolatedDistance, ceilingLayer);
            if (hit.collider != null)
            {
                StartSwing(hit.point);
                yield break;
            }

            yield return null;
        }

        ReleaseRope();
    }

    public void StartSwing(Vector2 hitPoint)
    {
        Debug.Log("Ceiling detected! Starting swing.");
        ropeAnchor = hitPoint + Vector2.down * 0.1f;
        distanceJoint.connectedAnchor = ropeAnchor;
        distanceJoint.distance = Vector2.Distance(handPosition.position, ropeAnchor);
        distanceJoint.enabled = true;
        isSwinging = true;
    }
    public void ReleaseRope()
    {
        Debug.Log("Swing released.");
        lineRenderer.enabled = false;
        distanceJoint.enabled = false;
        isSwinging = false;
    }
  
}