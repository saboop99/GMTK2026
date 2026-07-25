using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public string sceneName;

    public void LoadScenes(string ActualSceneName)
    {
        sceneName = ActualSceneName;
        SceneManager.LoadScene(ActualSceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
