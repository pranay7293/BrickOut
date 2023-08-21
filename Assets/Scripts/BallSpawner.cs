using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{

    [SerializeField] private LayerMask layerMask;
    [SerializeField] Vector2 minMaxAnglel;
    [SerializeField] bool useRay;
    [SerializeField] bool useLines;
    [SerializeField] bool useDots;
    [SerializeField] LineRenderer line;
    [SerializeField] GameObject ballPrefab;
    [SerializeField] float force;
    [SerializeField] int ballCount;


    private RaycastHit2D ray;
    private float angle;

    public void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            ray = Physics2D.Raycast(transform.position, transform.up, 20.0f, layerMask);
            //Debug.DrawRay(transform.position, ray.point, Color.red);

            Vector2 reflectPos = Vector2.Reflect(new Vector3(ray.point.x, ray.point.y) - transform.position, ray.normal);
            Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
            Vector3 dir = Input.mousePosition - pos;

            angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg - 90f;

            if (angle >= minMaxAnglel.x && angle <= minMaxAnglel.y)
            {
                if (useRay)
                {
                    Debug.DrawRay(transform.position, transform.up * ray.distance, Color.red);
                    Debug.DrawRay(ray.point, reflectPos.normalized * 2f, Color.green);
                }
                if (useLines)
                {
                    line.SetPosition(0, transform.position);
                    line.SetPosition(1, ray.point);
                    line.SetPosition(2, ray.point + reflectPos.normalized * 2f);
                }
                if (useDots)
                {
                    DotsLine.instance.DrawDottedLine(transform.position, ray.point);
                    DotsLine.instance.DrawDottedLine(ray.point, ray.point + reflectPos.normalized * 2f);

                }
                transform.rotation = Quaternion.AngleAxis(angle, transform.forward);
                 
            }


        }


    }
}
