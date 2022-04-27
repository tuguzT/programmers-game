using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menu
{
    public class MainMenu : MonoBehaviour
    {
        public void Play()
        {
            SceneManager.LoadScene("Choose Game Mode Menu Scene");
        }

        public void Quit()
        {
            print("Quit the game");
            Application.Quit();
        }
    }
}
