using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menu
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
            print("Create room");
        }

        public void ConnectToRoom()
        {
            print("Connect to room");
        }
    }
}
