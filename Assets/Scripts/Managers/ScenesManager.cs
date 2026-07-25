using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    private string sceneName;
    private GameObject Credits;
    public GameObject Menu;

    public void LoadScenes(string ActualSceneName)
    {
        sceneName = ActualSceneName;
        SceneManager.LoadScene(ActualSceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void EnableCredits(GameObject credits)
    {
        this.Credits = credits;
        credits.SetActive(true);
        Menu.SetActive(false);
    }

    public void EnableMenu(GameObject menu)
    {
        this.Menu = menu;
        menu.SetActive(true);
        Credits.SetActive(false);
    }
}
