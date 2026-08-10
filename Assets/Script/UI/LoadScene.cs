using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }
    public void ReturnTitle()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene("Title");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
