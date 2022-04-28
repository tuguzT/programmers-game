using System;
using Model;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Scene
{
    public class CreateRoomMenu : MonoBehaviourPunCallbacks
    {
        [SerializeField] private TMP_InputField roomNameInputField;

        [SerializeField] private Slider slider;

        [SerializeField] private TMP_Dropdown dropdown;

        public void CreateRoom()
        {
            var manager = GameManager.Instance;
            manager.PlayerCount = (byte) slider.value;
            manager.Difficulty = dropdown.value switch
            {
                0 => Difficulty.Easy,
                1 => Difficulty.Hard,
                _ => throw new ArgumentOutOfRangeException(nameof(dropdown.value), dropdown.value, null)
            };

            var roomOptions = new RoomOptions
            {
                MaxPlayers = manager.PlayerCount
            };
            PhotonNetwork.CreateRoom(roomNameInputField.text, roomOptions);
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
    }
}
