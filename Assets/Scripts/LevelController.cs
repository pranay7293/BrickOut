using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public int numOfBricks;
    private bool gameOver;
    [SerializeField] private bool levelComplete;
    [SerializeField] GameObject brickGrid;
    [SerializeField] GameObject gameOverMenu;
    [SerializeField] GameObject levelCompleteMenu;

    //private void Awake()
    // {
    // gameOver = false;
    //levelComplete = false;
    //numOfBricks = LevelManager.Instance.GetNumberOfBricks(SceneManager.GetActiveScene().buildIndex);
    //}
    private void Update()
    {
        if (numOfBricks == 0)
        {
            levelComplete = true;
        }
        if(brickGrid.transform.position.y < -4f)
        {
            gameOver = true;
        }  

        if (gameOver)
        {
            Time.timeScale = 0.0f;
            gameOverMenu.SetActive(true);

        }
        if (levelComplete)
        {
            levelCompleteMenu.SetActive(true);
        }
    }
    public void GameOver()
    {
        gameOver = true;
    }
    public void LevelComplete() 
    { 
        levelComplete = true; 
    }
    public void DecreaseBricks()
    {
        numOfBricks--;
    }
    public void MoveBricks()
    {
        Vector3 temp = brickGrid.transform.position;
        temp.y -= 1;
        brickGrid.transform.position = temp;
    }

}
