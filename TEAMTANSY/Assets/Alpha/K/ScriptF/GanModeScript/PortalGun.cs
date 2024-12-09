using UnityEngine;

public class PortalGun : MonoBehaviour
{
    public GameObject bluePortalPrefab;
    public GameObject orangePortalPrefab;
    public LayerMask wallLayer;
    public float rayDistance = 10f;   // Rayの最大距離
    public bool shootRight = true;    // 右向きにRayを飛ばすか（falseで左向き）

    private GameObject bluePortal;
    private GameObject orangePortal;
    private Vector2 direction;
    private PlayerController PC;
    private void Start()
    {
        PC = FindAnyObjectByType<PlayerController>();
    }
    void Update()
    {
      if (PC.axisH > 0.0f)
        {
            direction = Vector2.right;
        }
      if (PC.axisH < 0.0f)
        {
            direction = Vector2.left;
        }
    }

    public void CreatePortal()
    {
        // プレイヤーの位置をRayの始点に設定
        Vector2 origin = transform.position;

        // Rayの方向を設定（右向きまたは左向き）
       

        // デバッグ用のRayの可視化
        Debug.DrawRay(origin, direction * rayDistance, Color.red, 1.0f);

        // Raycastを発射
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, rayDistance, wallLayer);

        if (hit.collider != null)  // 壁に当たった場合
        {
            Debug.Log("壁にヒットしました: " + hit.collider.name);

            // 既存のポータルがあれば削除
            if (bluePortal != null) Destroy(bluePortal);
            if (orangePortal != null) Destroy(orangePortal);

            // 青ポータルをヒット位置に配置
            bluePortal = Instantiate(bluePortalPrefab, hit.point, Quaternion.identity);

            // 壁の反対側にオレンジポータルを配置
            Vector2 oppositePosition = hit.point + hit.normal * -2.0f;
            orangePortal = Instantiate(orangePortalPrefab, oppositePosition, Quaternion.identity);

            // ポータル同士をリンク
            var bluePortalScript = bluePortal.GetComponent<Portal>();
            var orangePortalScript = orangePortal.GetComponent<Portal>();
            if (bluePortalScript != null && orangePortalScript != null)
            {
                bluePortalScript.linkedPortal = orangePortalScript;
                orangePortalScript.linkedPortal = bluePortalScript;
            }
        }
        else
        {
            Debug.Log("Rayが何にもヒットしませんでした");
        }
    }
}
