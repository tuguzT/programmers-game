using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scene
{
    public class CreateRoomMenu : MonoBehaviourPunCallbacks
    {
        [SerializeField] private TMP_InputField roomNameInputField;

        public void CreateRoom()
        {
            PhotonNetwork.CreateRoom(roomNameInputField.text);
        }

        public void Back()
        {
            SceneManager.LoadScene("Choose Game Mode Menu Scene");
        }

        public override void OnJoinedRoom()
        {
            PhotonNetwork.LoadLevel("Game Scene");
            MusicManager.Instance.PlayLevelMusic();
        }
    }
}
