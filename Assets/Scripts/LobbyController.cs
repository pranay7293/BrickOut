using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyController : MonoBehaviour
{
    public Button buttonLevel;
    public Button buttonPlay;
    public Button buttonSettings;
    public Button buttonResetLevels;
    public Button Backbutton;
    public Button Backbutton2;
    public Button Music;
    public Button Sound;
    public Image spriteImageM;
    public Image spriteImageS;

    public Sprite newMusicSprite;
    public Sprite defaultMusicSprite;
    public Sprite newSoundSprite;
    public Sprite defaultSoundSprite;

    private bool isDefaultMusicSprite = true;
    private bool isDefaultSoundSprite = true;

    public GameObject LevelSelection;
    public GameObject Settingsmenu;

    void Awake()
    {
        buttonLevel.onClick.AddListener(levelSelection);
        buttonPlay.onClick.AddListener(PlayFirstLevel);
        buttonSettings.onClick.AddListener(SettingsMenu);
        buttonResetLevels.onClick.AddListener(ResetLevels);
        Backbutton.onClick.AddListener(BackButton);
        Backbutton2.onClick.AddListener(BackButton);
        Music.onClick.AddListener(ChangeMusicSprite);
        Sound.onClick.AddListener(ChangeSoundSprite);
    }

    private void ChangeSoundSprite()
    {
        if (isDefaultSoundSprite)
        {
            spriteImageS.sprite = newSoundSprite;
            SoundManager.Instance.StopSfx();
        }
        else
        {
            spriteImageS.sprite = defaultSoundSprite;
            SoundManager.Instance.StartSfx();
        }
        isDefaultSoundSprite = !isDefaultSoundSprite;
    }

    private void ChangeMusicSprite()
    {
        if (isDefaultMusicSprite)
        {
            spriteImageM.sprite = newMusicSprite;
            SoundManager.Instance.StopMusic();
        }
        else
        {
            spriteImageM.sprite = defaultMusicSprite;
            SoundManager.Instance.StartMusic();
        }
        isDefaultMusicSprite = !isDefaultMusicSprite;
    }

    private void BackButton()
    {
       SoundManager.Instance.PlaySound(Sounds.BackExit);
        Settingsmenu.SetActive(false);
        LevelSelection.SetActive(false);
    }

    private void ResetLevels()
    {
        SoundManager.Instance.PlaySound(Sounds.Menu);
        LevelManager.Instance.LevelsReset();
    }

    private void SettingsMenu()
    {
        SoundManager.Instance.PlaySound(Sounds.Menu);
        Settingsmenu.SetActive(true);
    }

    private void levelSelection()
    {
        SoundManager.Instance.PlaySound(Sounds.Menu);
        LevelSelection.SetActive(true);
    }

    private void PlayFirstLevel()
    {
        SoundManager.Instance.PlaySound(Sounds.PlayPause);
        SceneManager.LoadScene(1);
    }    

}
