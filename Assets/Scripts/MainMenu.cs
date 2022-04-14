using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("Game Scene");
    }

    public void Quit()
    {
        print("Quit the game");
        Application.Quit();
    }
}
