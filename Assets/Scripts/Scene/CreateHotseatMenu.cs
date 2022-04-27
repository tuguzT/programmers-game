using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

namespace Scene
{
    public class CreateHotseatMenu : MonoBehaviourPunCallbacks
    {
        public void CreateHotseat()
        {
            if (PhotonNetwork.IsConnected)
                PhotonNetwork.Disconnect();
            else
                OnDisconnected(DisconnectCause.DisconnectByClientLogic);
        }

        public void Back()
        {
            SceneManager.LoadScene("Choose Game Mode Menu Scene");
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            if (cause != DisconnectCause.DisconnectByClientLogic) return;

            PhotonNetwork.OfflineMode = true;
            PhotonNetwork.LoadLevel("Level Scene");
            MusicManager.Instance.PlayLevelMusic();
        }
    }
}
