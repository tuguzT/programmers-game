using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scene
{
    public class MainMenu : MonoBehaviour
    {
        private void Start()
        {
            MusicManager.Instance.PlayMenuMusic();
        }

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
