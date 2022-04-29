using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scene
{
    public class JoinRoomMenu : MonoBehaviourPunCallbacks
    {
        [SerializeField] private TMP_InputField roomNameInputField;
        
        public void JoinRoom()
        {
            PhotonNetwork.JoinRoom(roomNameInputField.text);
        }

        public void Back()
        {
            SceneManager.LoadScene("Choose Game Mode Menu Scene");
        }

        public override void OnJoinedRoom()
        {
            PhotonNetwork.LoadLevel("Level Scene");
            MusicManager.Instance.PlayLevelMusic();
        }

        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            Debug.LogWarning($"Return code: {returnCode}. Message: \"{message}\"");
        }
    }
}
