using System.Collections;
using UnityEngine;

public class PlayerRopeSwing : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public DistanceJoint2D distanceJoint;
    public Transform player;
    public Transform handPosition;
    public LayerMask ceilingLayer;
    public LayerMask wallLayer; // Ground layer to detect ground collisions
    public float maxRopeLength = 15f;
    public float ropeExtendSpeed = 5f;
    public float launchAngle = 45f;
    public float ropeShortenSpeed = 10f;
    public float shortenRange = 6.0f;
    public bool isSwinging = false;
    public Rigidbody2D pendulumRigidbody;

    private Vector2 ropeDirection;
    private Vector2 ropeAnchor;
    private Transform movingPlatform; // 動くブロックのTransform
    private Vector2 platformOffset;   // 動くブロックとの相対位置

    void Start()
    {
        lineRenderer.enabled = false;
        distanceJoint.enabled = false;
    }

    void Update()
    {
        if (lineRenderer.enabled && isSwinging)
        {
            if (movingPlatform != null)
            {
                // 動くブロックに接続している場合、接続点を更新
                ropeAnchor = (Vector2)movingPlatform.position + platformOffset;
                distanceJoint.connectedAnchor = ropeAnchor;
            }
            // ロープの描画更新
            lineRenderer.SetPosition(0, handPosition.position);
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
        ropeDirection = new Vector2(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians)).normalized;

        Vector3 ropeEndPoint = handPosition.position + (Vector3)ropeDirection * maxRopeLength;

        lineRenderer.enabled = true;
        StartCoroutine(AnimateRopeExtension(ropeEndPoint));
    }

    private IEnumerator AnimateRopeExtension(Vector3 targetPoint)
    {
        float distanceCovered = 0f;

        while (distanceCovered < maxRopeLength)
        {
            distanceCovered += ropeExtendSpeed * Time.deltaTime;
            float interpolatedDistance = Mathf.SmoothStep(0, maxRopeLength, distanceCovered / maxRopeLength);

            Vector3 nextPosition = (Vector2)handPosition.position + ropeDirection * interpolatedDistance;
            lineRenderer.SetPosition(1, nextPosition);

            RaycastHit2D hit = Physics2D.Raycast(handPosition.position, ropeDirection, interpolatedDistance, ceilingLayer);
            if (hit.collider != null)
            {
                StartSwing(hit.collider.transform, hit.point);
                yield break;
            }

            yield return null;
        }

        ReleaseRope();
    }

    private void StartSwing(Transform hitTransform, Vector2 hitPoint)
    {
        Debug.Log("Swing started.");

        Collider2D hitcollider = Physics2D.OverlapPoint(hitPoint, ceilingLayer);
        if (hitcollider != null && hitcollider.CompareTag("Movebrock"))
        {
            movingPlatform = hitTransform;
            platformOffset = hitPoint - (Vector2)movingPlatform.position;
        }

        ropeAnchor = hitPoint;
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
        movingPlatform = null;
    }
}
