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
    public class CreateHotseatMenu : MonoBehaviourPunCallbacks
    {
        [SerializeField] private Slider slider;

        [SerializeField] private TMP_Dropdown dropdown;

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

            var manager = GameManager.Instance;
            manager.PlayerCount = (byte) slider.value;
            manager.Difficulty = dropdown.value switch
            {
                0 => Difficulty.Easy,
                1 => Difficulty.Hard,
                _ => throw new ArgumentOutOfRangeException(nameof(dropdown.value), dropdown.value, null)
            };

            PhotonNetwork.OfflineMode = true;
            PhotonNetwork.LoadLevel("Level Scene");
            MusicManager.Instance.PlayLevelMusic();
        }
    }
}
