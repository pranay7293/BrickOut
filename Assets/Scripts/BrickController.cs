using UnityEngine;

public class BrickController : MonoBehaviour
{
    [SerializeField] private ParticleSystem explosion1;
    [SerializeField] private ParticleSystem explosion2;
    [SerializeField] private ParticleSystem explosion3;
    [SerializeField] private int brickNumber;
    [SerializeField] private GameObject overlay;
    [SerializeField] TextMesh text;
    [SerializeField] private LevelController levelController;
    [SerializeField] BrickType type;
    [SerializeField] private LayerMask layerMask;


    private void Start()
    {
        if (type != BrickType.Destroyer)
        {
            text.text = brickNumber.ToString();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent < BallCtrl>() != null)
        {
            SoundManager.Instance.PlaySound(Sounds.BrickHit);
            brickNumber--;
            if (type == BrickType.Stationary && brickNumber == 0)
            {
                DestroyBrick();
            }            
           else if (type == BrickType.Destroyer && brickNumber == 0)
            {
                Demolish();
                DestroyBrick();
            }
            else if (type == BrickType.Smashing && brickNumber == 0)
            {
                SmashRow();
                DestroyBrick();
            }
            else
            {
                text.text = brickNumber.ToString();
            }
            overlay.SetActive(true);
            Invoke("RemoveOverlay", 0.25f);
        }
    }

    private void SmashRow()
    {
        RaycastHit2D[] rightHits = Physics2D.RaycastAll(transform.position, Vector2.right, 50f, layerMask);
        RaycastHit2D[] leftHits = Physics2D.RaycastAll(transform.position, -Vector2.right, 50f, layerMask);

        foreach (RaycastHit2D hit in rightHits)
        {
            if (hit.collider != null && hit.collider.gameObject != gameObject)
            {
                hit.collider.GetComponent<BrickController>().DestroyBrick();
            }
        }

        foreach (RaycastHit2D hit in leftHits)
        {
            if (hit.collider != null && hit.collider.gameObject != gameObject)
            {
                hit.collider.GetComponent<BrickController>().DestroyBrick();
            }
        }
    }

    private void Demolish()
    {

        RaycastHit2D[] rightHits = Physics2D.RaycastAll(transform.position, Vector2.right, 1.0f, layerMask);
        RaycastHit2D[] leftHits = Physics2D.RaycastAll(transform.position, Vector2.left, 1.0f, layerMask);
        RaycastHit2D[] upHits = Physics2D.RaycastAll(transform.position, Vector2.up, 1.0f, layerMask);
        RaycastHit2D[] downHits = Physics2D.RaycastAll(transform.position, Vector2.down, 1.0f, layerMask);
       

        foreach (RaycastHit2D hit in rightHits)
        {
            if (hit.collider != null && hit.collider.gameObject != gameObject)
            {
                hit.collider.GetComponent<BrickController>().DestroyBrick();
            }
        }
        foreach (RaycastHit2D hit in leftHits)
        {
            if (hit.collider != null && hit.collider.gameObject != gameObject)
            {
                hit.collider.GetComponent<BrickController>().DestroyBrick();
            }
        }
        foreach (RaycastHit2D hit in upHits)
        {
            if (hit.collider != null && hit.collider.gameObject != gameObject)
            {
                hit.collider.GetComponent<BrickController>().DestroyBrick();
            }
        }
        foreach (RaycastHit2D hit in downHits)
        {
            if (hit.collider != null && hit.collider.gameObject != gameObject)
            {
                hit.collider.GetComponent<BrickController>().DestroyBrick();
            }
        }
    }

    public void DestroyBrick()
    {
        levelController.DecreaseBricks();

        if (type == BrickType.Stationary)
        {
            GameObject temp = Instantiate(explosion1.gameObject, transform.position, Quaternion.identity);
            SoundManager.Instance.PlaySound(Sounds.BrickDestroy1);
            Destroy(gameObject);
            Destroy(temp, 2.5f);
        }
        else if (type == BrickType.Destroyer)
        {
            GameObject temp = Instantiate(explosion2.gameObject, transform.position, Quaternion.identity);
            SoundManager.Instance.PlaySound(Sounds.BrickDestroy2);
            Destroy(gameObject);
            Destroy(temp, 2.5f);
        }
        else if (type == BrickType.Smashing)
        {
            GameObject temp = Instantiate(explosion3.gameObject, transform.position, Quaternion.identity);
            SoundManager.Instance.PlaySound(Sounds.BrickDestroy2);
            Destroy(gameObject);
            Destroy(temp, 2.5f);
        }
    }

    private void RemoveOverlay()
    {
        overlay.SetActive(false);
    }
   
}
public enum BrickType
{
    Stationary,
    Destroyer,
    Smashing
}
