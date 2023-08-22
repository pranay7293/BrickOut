using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallShooter : MonoBehaviour
{

    [SerializeField] private LayerMask layerMask;
    [SerializeField] Vector2 minMaxAnglel;
    [SerializeField] GameObject ballPrefab;
    [SerializeField] float force;
    [SerializeField] int ballCount;
    [SerializeField] float resetDelay = 3.0f; // Adjust this as needed


    private int currentBallCount;
    private int ballsReturned;
    private Vector3 initialPosition;
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

            angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;

            if (angle >= minMaxAnglel.x && angle <= minMaxAnglel.y)
            {
                DotsLine.instance.DrawDottedLine(transform.position, ray.point);
                DotsLine.instance.DrawDottedLine(ray.point, ray.point + reflectPos.normalized * 2f);
                transform.rotation = Quaternion.AngleAxis(angle, transform.forward);
            }

        }


    }
    public void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            StartCoroutine(ShootBalls());
        }
    }
    public IEnumerator ShootBalls()
    {
        for (int i = 0; i < ballCount; i++)
        {
            yield return new WaitForSeconds(0.2f);
            GameObject ball = Instantiate(ballPrefab, transform.position, Quaternion.identity);
            ball.GetComponent<Rigidbody2D>().AddForce(transform.up * force);
        }
        // Wait for the specified delay, then reset the position
        yield return new WaitForSeconds(resetDelay);
        ResetShooterPosition();
    }
    private void ResetShooterPosition()
    {
        transform.position = initialPosition;
    }

    public void IncreaseBalls()
    {
        currentBallCount++;
        ballsReturned++;
    }
}
