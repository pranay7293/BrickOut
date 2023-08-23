using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    [SerializeField] private int numOfBricks;
    [SerializeField] GameObject brickGrid;
    [SerializeField] GameObject gameOverMenu;
    [SerializeField] GameObject levelCompleteMenu;  

    
    private void Update()
    {
        if (numOfBricks == 0)
        {
            levelCompleteMenu.SetActive(true);
        }
        if (brickGrid.transform.position.y < -4f)
        {
            gameOverMenu.SetActive(true);
        }       
    }
    
    public void DecreaseBricks()
    {
        numOfBricks--;
    }
    public void MoveBricks()
    {
        Vector2 temp = brickGrid.transform.position;
        temp.y--;
        brickGrid.transform.position = temp;
    }

}
