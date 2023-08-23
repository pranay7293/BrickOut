using System.Collections;
using UnityEngine;

public class BallShooter : MonoBehaviour
{

    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Vector2 minMaxAnglel;
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private float force;
    [SerializeField] private int ballCount;
    [SerializeField] private float speed;
    [SerializeField] LevelController levelController;

    public int currentBallCount;
    public int ballsReturned;
    private bool hasBallsReturned;
    private bool isShooting = false;
    private bool decreasedHeight;

    private RaycastHit2D ray;
    private float angle;

    private void Start()
    {
        ballsReturned = 0;
        hasBallsReturned = true;
        decreasedHeight = true;
        currentBallCount = ballCount;
    }

    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        if (!isShooting)
        {
            Move(horizontal);
        }

        if (ballsReturned == currentBallCount)
        {
            Time.timeScale = 1.0f;
            decreasedHeight = false;
            hasBallsReturned = true;
            ballsReturned = 0;
        }

        if (hasBallsReturned && !decreasedHeight)
        {
            levelController.MoveBricks();
            decreasedHeight = true;
        }

        if (Input.GetMouseButton(0) && !isShooting)
        {
            AimToShoot();
        }

        if (Input.GetMouseButtonUp(0) && !isShooting && hasBallsReturned)
        {
            isShooting = true;
            hasBallsReturned = false;
            StartCoroutine(ShootBalls());
        }
    }

    private void AimToShoot()
    {
        ray = Physics2D.Raycast(transform.position, transform.up, 20.0f, layerMask);

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

    private void Move(float _horizontal)
    {
        Vector3 temp = transform.position;
        temp.x = Mathf.Clamp(temp.x + speed * _horizontal * Time.deltaTime, -5, 5);
        transform.position = temp;
    }
    
    public IEnumerator ShootBalls()
    {
        ballsReturned = 0;
        for (int i = 0; i < ballCount; i++)
        {            
            yield return new WaitForSeconds(0.2f);
            GameObject ball = Instantiate(ballPrefab, transform.position, Quaternion.identity);
            ball.GetComponent<Rigidbody2D>().AddForce(transform.up * force);
        }
        isShooting = false;
    }
    
}
