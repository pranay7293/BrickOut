using System;
using UnityEngine;

public class BrickController : MonoBehaviour
{
    [SerializeField] private ParticleSystem explosion1;
    [SerializeField] private ParticleSystem explosion2;
    [SerializeField] private int brickNumber;
    [SerializeField] private GameObject overlay;
    [SerializeField] TextMesh text;
    [SerializeField] private LevelController levelController;
    [SerializeField] BrickType type;
    [SerializeField] private LayerMask layerMask;
    private Vector3 initialPos;



    private void Start()
    {
        if (type != BrickType.Destroyer)
        {
            text.text = brickNumber.ToString();
        }
        initialPos = transform.position;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent < BallCtrl>() != null)
        {
            //SoundManager.Instance.PlayFX(SoundType.Brick_1_Hit);
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
            else
            {
                text.text = brickNumber.ToString();
            }
            overlay.SetActive(true);
            Invoke("RemoveOverlay", 0.25f);
        }
    }
    private void Demolish()
    {
        RaycastHit2D[] rightHits = Physics2D.RaycastAll(initialPos, Vector2.right, 50f, layerMask);
        RaycastHit2D[] leftHits = Physics2D.RaycastAll(initialPos, -Vector2.right, 50f, layerMask);
        RaycastHit2D[] topHits = Physics2D.RaycastAll(initialPos, Vector2.up, 50f, layerMask);
        RaycastHit2D[] bottomHits = Physics2D.RaycastAll(initialPos, -Vector2.up, 50f, layerMask);

        int bricks = (rightHits.Length - 1) + (leftHits.Length - 1) + (topHits.Length - 1) + (bottomHits.Length - 1) + 1;
        for (int i = 0; i < rightHits.Length; i++)
        {
            RaycastHit2D hit = rightHits[i];
            hit.collider.GetComponent<BrickController>().DestroyBrick();
        }
        for (int i = 0; i < leftHits.Length; i++)
        {
            RaycastHit2D hit = leftHits[i];
            hit.collider.GetComponent<BrickController>().DestroyBrick();
        }
        for (int i = 0; i < topHits.Length; i++)
        {
            RaycastHit2D hit = topHits[i];
            hit.collider.GetComponent<BrickController>().DestroyBrick();
        }
        for (int i = 0; i < bottomHits.Length; i++)
        {
            RaycastHit2D hit = bottomHits[i];
            hit.collider.GetComponent<BrickController>().DestroyBrick();
        }
        for (int i = 0; i < bricks; i++)
        {
            levelController.DecreaseBricks();
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
    }

    private void RemoveOverlay()
    {
        overlay.SetActive(false);
    }
   
}
public enum BrickType
{
    Stationary,
    Destroyer
}
