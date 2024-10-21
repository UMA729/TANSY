using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    public Transform firePoint;
    public float maxDistance = 10f; // �t�b�N�̍ő勗��
    public float hookSpeed = 20f;
    public LineRenderer lineRenderer; // ���C�������_���[
    public LayerMask grappleableLayer; // �O���b�v���\�ȃ��C���[

    private Rigidbody2D rb;
    private Vector2 grapplePoint; // �t�b�N������������_
    private bool isGrappling;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (!isGrappling)
            {
                StartGrapple();
            }
            else
            {
                StopGrapple();
            }
        }

        if (isGrappling)
        {
            lineRenderer.SetPosition(0, firePoint.position);
            lineRenderer.SetPosition(1, grapplePoint);

            Vector2 grappleDir = (grapplePoint - (Vector2)firePoint.position).normalized;
            rb.velocity = grappleDir * hookSpeed;
        }
    }

    void StartGrapple()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, maxDistance, grappleableLayer);
        if (hit)
        {
            grapplePoint = hit.point;
            isGrappling = true;
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, grapplePoint);
        }
    }

    void StopGrapple()
    {
        isGrappling = false;
        lineRenderer.positionCount = 0;
    }

    void LateUpdate()
    {
        if (isGrappling)
        {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, grapplePoint);
        }
    }
}
