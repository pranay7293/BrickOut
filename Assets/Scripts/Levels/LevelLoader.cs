using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LevelLoader : MonoBehaviour
{
    private Button button;
    public string LevelName;

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
                Debug.Log("can't play this level till you unlock it");
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
}