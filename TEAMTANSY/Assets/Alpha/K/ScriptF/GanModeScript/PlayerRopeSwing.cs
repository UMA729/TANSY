using System.Collections;
using UnityEngine;

public class PlayerRopeSwing : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public DistanceJoint2D distanceJoint;
    public Transform player;
    public Transform handPosition;
    public LayerMask ceilingLayer;       // 刺さる天井のレイヤー
    public float maxRopeLength = 15f;    // 伸びきるまでの長さ
    public float ropeExtendSpeed = 5f;   // ロープがでる速さ
    public float launchAngle = 45f;      // 射出角度
    public float ropeShortenSpeed = 10f; // ロープを縮める祖k度
    public float shortenRange = 6.0f;    // ロープを縮める長さ
    public float Movingshorten = 4.0f;
    public bool isSwinging;              // スウィング中か否か
    private bool isMoving;
    public Rigidbody2D pendulumRigidbody;// 振り子を行うオブジェクトのrb

    private Vector2 ropeDirection;       // ロープの距離
    private Vector2 ropeAnchor;          // 接続点
    private Transform movingPlatform;    // 動くブロックのTransform
    private Vector2 platformOffset;      // 動くブロックとの相対位置

    void Start()
    {
        lineRenderer.enabled  = false;   //----------------------
        distanceJoint.enabled = false;   //
        isSwinging            = false;   // 初期化
        isMoving              = false;   //
        movingPlatform        =  null;   //----------------------
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
            //// ロープの描画更新
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

        //ロープの角度を更新
        ropeDirection = new Vector2(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians)).normalized;
        //ロープが伸びきる位置を設定
        Vector3 ropeEndPoint = handPosition.position + (Vector3)ropeDirection * maxRopeLength;
        //linerendererを無効化
        lineRenderer.enabled = true;
        StartCoroutine(AnimateRopeExtension(ropeEndPoint));
    }

    private IEnumerator AnimateRopeExtension(Vector3 targetPoint)
    {
        float distanceCovered = 0f;

        //ロープが伸びきるまで回す
        while (distanceCovered < maxRopeLength)
        {
            distanceCovered += ropeExtendSpeed * Time.deltaTime; //ロープが伸びきるまでの時間を更新
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
        //伸びきるとロープを戻す
        ReleaseRope();
    }

    private void StartSwing(Transform hitTransform, Vector2 hitPoint)
    {
        Collider2D hitcollider = Physics2D.OverlapPoint(hitPoint, ceilingLayer);

        if (hitcollider != null)//Movebrockというタグにヒットしたら入る
        {
            Debug.Log("colliderを検知しました。");
            if (hitcollider.CompareTag("Movebrock"))
            {
                //movingPlatformにロープが刺さった位置を代入
                movingPlatform = hitTransform;
                //刺さる位置とブロック中心座標を元にブロックとロープがどのくらい離れているのかの相対位置
                platformOffset = hitPoint - (Vector2)movingPlatform.position;
                ropeShortenSpeed += Movingshorten;
                isMoving = true;
            }
        }
        else if(hitcollider == null)
        {

            Debug.Log("nohit");
        }

        ropeAnchor = hitPoint;                                                        //接続点を更新
        distanceJoint.connectedAnchor = ropeAnchor;                                   //接続点を軸に振り子動作を行う
        distanceJoint.distance = Vector2.Distance(handPosition.position, ropeAnchor); //ロープが刺さった位置とプレイヤーとの距離
        distanceJoint.enabled = true;                                                 //distanceジョイントを無効化
        isSwinging = true;                                                            //スウィング中
    }

    public void ReleaseRope()
    {
        if (isMoving)
        {
            ropeShortenSpeed -= Movingshorten;
            isMoving = false;
        }
        lineRenderer.enabled = false; //linerendererを有効
        distanceJoint.enabled = false;//distancejointを有効
        isSwinging = false;          
        movingPlatform = null; 
    }
}
