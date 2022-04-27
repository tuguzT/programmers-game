using Photon.Pun;
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
            SceneManager.LoadScene("Create Hotseat Menu Scene");
        }

        public void CreateRoom()
        {
            PhotonNetwork.OfflineMode = false;
            GameManager.Instance.LoadSceneName = "Create Room Menu Scene";
            SceneManager.LoadScene("Connect To Server Scene");
        }

        public void JoinRoom()
        {
            PhotonNetwork.OfflineMode = false;
            GameManager.Instance.LoadSceneName = "Join Room Menu Scene";
            SceneManager.LoadScene("Connect To Server Scene");
        }
    }
}
