using Photon.Pun;
using UnityEngine.SceneManagement;

namespace Scene
{
    public class ConnectToServer : MonoBehaviourPunCallbacks
    {
        private void Awake()
        {
            if (PhotonNetwork.IsConnected) OnJoinedLobby();
        }

        private void Start()
        {
            PhotonNetwork.ConnectUsingSettings();
        }

        public override void OnConnectedToMaster()
        {
            PhotonNetwork.JoinLobby();
        }

        public override void OnJoinedLobby()
        {
            var sceneName = GameManager.Instance.LoadSceneName;
            SceneManager.LoadScene(sceneName);
        }
    }
}
