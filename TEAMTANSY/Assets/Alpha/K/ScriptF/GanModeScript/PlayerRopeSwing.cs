using System.Collections;
using UnityEngine;

public class PlayerRopeSwing : MonoBehaviour
{
    //定数
    public float maxRopeLength = 15f;      // 伸びきるまでの長さ
    public float ropeExtendSpeed = 5f;     // フックがでる速さ
    public float launchAngle = 45f;        // 射出角度
    public float ropeShortenSpeed = 10f;   // フックを縮める速度
    public float shortenRange = 6.0f;      // フックを縮める長さ
    public float Movingshorten = 4.0f;     // 動くブロックにフックがついている時に短くする速度

    //コンポーネント
    public LineRenderer lineRenderer;      //フックを描画するために必要
    public DistanceJoint2D distanceJoint;  //振り子動作に必要
    public AudioClip RopeSound;            //フックを出す音
    public Transform player;               //主人公のオブジェクト座標
    public Transform handPosition;         //主人公の手座標
    public Rigidbody2D pendulumRigidbody;  // 振り子を行うオブジェクトのRb

    public LayerMask ceilingLayer;         // 刺さる天井のレイヤー

    //フラグ
    public bool isSwinging;                // スウィングフラグ
    private bool isMoving;                 // オブジェクトが動いているフラグ
   
    //フック座標など
    private Vector2 ropeDirection;         // フックの距離
    private Vector2 ropeAnchor;            // 接続点
    private Transform movingPlatform;      // 動くブロックのTransform
    private Vector2 platformOffset;        // 動くブロックとの相対位置

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
        //スウィングをしていなると
        if (lineRenderer.enabled && isSwinging)
        {
            //動くブロックだと
            if (movingPlatform != null)
            {
                // 動くブロックに接続している場合、接続点を更新
                ropeAnchor = (Vector2)movingPlatform.position + platformOffset;
                distanceJoint.connectedAnchor = ropeAnchor;
            }
            // フックの描画更新
            Vector2 endPosition = (Vector2)handPosition.position + (ropeAnchor - (Vector2)handPosition.position).normalized * distanceJoint.distance;
            lineRenderer.SetPosition(1, endPosition);

            //フックを一定まで短くする
            if (distanceJoint.distance > shortenRange)
            {
                distanceJoint.distance -= ropeShortenSpeed * Time.deltaTime;
            }
        }
        //フックが出る位置を更新
        lineRenderer.SetPosition(0, handPosition.position);
    }

    public void ExtendRope()
    {
        float angleInRadians = launchAngle * Mathf.Deg2Rad;

        //フックの正規化
        ropeDirection = new Vector2(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians)).normalized;
        //フックが伸びきる位置を設定
        Vector3 ropeEndPoint = handPosition.position + (Vector3)ropeDirection * maxRopeLength;
        //linerendererを無効化
        lineRenderer.enabled = true;
        StartCoroutine(AnimateRopeExtension(ropeEndPoint));
    }

    private IEnumerator AnimateRopeExtension(Vector3 targetPoint)
    {
        AudioSource.PlayClipAtPoint(RopeSound,handPosition.position); //ロープ発射音

        float distanceCovered = 0f;

        //フックが伸びきるまで回す
        while (distanceCovered < maxRopeLength)
        {
            //フックが伸びる速さ
            distanceCovered += ropeExtendSpeed * Time.deltaTime; 

            //伸び途中のフックの長さを補間
            float interpolatedDistance = Mathf.SmoothStep(0, maxRopeLength, distanceCovered / maxRopeLength);

            //フックが現在どれくらい伸びているか
            Vector3 nextPosition = (Vector2)handPosition.position + ropeDirection * interpolatedDistance;

            //現在のフックの長さから描画
            lineRenderer.SetPosition(1, nextPosition);

            //フックがつくレイヤーを探すレイキャスト
            RaycastHit2D hit = Physics2D.Raycast(handPosition.position, ropeDirection, interpolatedDistance, ceilingLayer);

            //フックが出ているかのデバッグ
            Debug.DrawLine(handPosition.position, handPosition.position + (Vector3)ropeDirection * maxRopeLength, Color.red);

            //フックがヒットしたら
            if (hit.collider != null)
            {
                Debug.Log($"Hit point: {hit.point}");
                //スウィング開始（当たったコライダーと当たった位置座標値を渡す）
                StartSwing(hit.collider.transform, hit.point);
                yield break;
            }

            yield return null;
        }
        //伸びきった場合フックを戻す
        ReleaseRope();
    }

    private void StartSwing(Transform hitTransform, Vector2 hitPoint)
    {
        //フックが当たるコライダー
        Collider2D hitcollider = Physics2D.OverlapPoint(hitPoint, ceilingLayer);

        //Movebrockというタグにヒットしたら入る
        if (hitcollider != null)
        {
            Debug.Log("colliderを検知しました。");
            //コライダーのタグがMovebrockであれば
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
        //オブジェクトがなければ
        else if(hitcollider == null)
        {
            Debug.Log("nohit");
        }

        ropeAnchor = hitPoint;                                                        //接続点を更新
        distanceJoint.connectedAnchor = ropeAnchor;                                   //接続点を軸に振り子動作を行う
        distanceJoint.distance = Vector2.Distance(handPosition.position, ropeAnchor); //フックが刺さった位置とプレイヤーとの距離
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
