using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scene
{
    public class ChooseGameModeMenu : MonoBehaviour
    {
        public void Back()
        {
            SceneManager.LoadScene("Main Menu Scene");
        }

        public void HotSeat()
        {
            SceneManager.LoadScene("Game Scene");
            MusicManager.Instance.PlayLevelMusic();
        }

        public void CreateRoom()
        {
            GameManager.Instance.LoadSceneName = "Create Room Menu Scene";
            SceneManager.LoadScene("Connect To Server Screen");
        }

        public void JoinRoom()
        {
            GameManager.Instance.LoadSceneName = "Join Room Menu Scene";
            SceneManager.LoadScene("Connect To Server Screen");
        }
    }
}
