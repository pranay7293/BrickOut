using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    public Button buttonReplay;
    public Button buttonExit;
    public Button buttonReplay2;
    public Button buttonExit2;
    public Button nextLevel;
    public Button buttonResueme;
    public Button buttonExit3;
    public Button pauseGame;
    public GameObject pauseMenu;


    void Awake()
    {
        buttonReplay.onClick.AddListener(ReplayLevel);
        buttonExit.onClick.AddListener(ExitToLobby);
        buttonReplay2.onClick.AddListener(ReplayLevel);
        buttonExit2.onClick.AddListener(ExitToLobby);
        nextLevel.onClick.AddListener(NextLevelLoad);
        buttonResueme.onClick.AddListener(ResumeGame);
        buttonExit3.onClick.AddListener(ExitToLobby);
        pauseGame.onClick.AddListener(PauseGame);

    }

    private void PauseGame()
    {
        SoundManager.Instance.PlaySound(Sounds.PlayPause);
        pauseMenu.SetActive(true);
        Time.timeScale = 0.0f;
    }

    private void NextLevelLoad()
    {
        SoundManager.Instance.PlaySound(Sounds.PlayPause);
        LevelManager.Instance.MarkCurrentLevelComplete();
    }

    private void ResumeGame()
    {
        SoundManager.Instance.PlaySound(Sounds.PlayPause);
        pauseMenu.SetActive(false);
        Time.timeScale = 1.0f;
    }

    private void ReplayLevel()
    {
        SoundManager.Instance.PlaySound(Sounds.PlayPause);
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
    private void ExitToLobby()
    {
        SoundManager.Instance.PlaySound(Sounds.BackExit);
        SceneManager.LoadScene(0);
    }
}
