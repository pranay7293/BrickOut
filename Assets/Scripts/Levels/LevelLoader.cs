using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LevelLoader : MonoBehaviour
{   
    private Button button;
    public string LevelName;
    public TextMeshProUGUI levelUnlock;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(onClick);
    }

    private void onClick()
    {
        LevelStatus levelStatus = LevelManager.Instance.GetLevelStatus(LevelName);
        switch (levelStatus)
        {
            case LevelStatus.Locked:
                SoundManager.Instance.PlaySound(Sounds.Error);
                StartCoroutine(ClearLevelUnlockText());
                break;
            case LevelStatus.Unlocked:
                SoundManager.Instance.PlaySound(Sounds.LevelClick);
                SceneManager.LoadScene(LevelName);
                break;
            case LevelStatus.Completed:
                SoundManager.Instance.PlaySound(Sounds.LevelClick);
                SceneManager.LoadScene(LevelName);
                break;
        }
    }
    private IEnumerator ClearLevelUnlockText()
    {
        levelUnlock.text = "Complete previous level to Unlock this Level";

        yield return new WaitForSeconds(2.5f);

        levelUnlock.text = "";
    }
}