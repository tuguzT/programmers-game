using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scene
{
    public class PauseMenu : MonoBehaviourPunCallbacks
    {
        [SerializeField]
        private GameObject pauseMenu;

        private void Update()
        {
            if (!Input.GetKeyDown(KeyCode.Escape)) return;

            if (GameManager.Instance.IsGamePaused)
            {
                ReturnToGame();
                return;
            }
            PauseGame();
        }

        private void PauseGame()
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
            GameManager.Instance.IsGamePaused = true;
            MusicManager.Instance.PauseMusic();
        }

        public void ReturnToGame()
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
            GameManager.Instance.IsGamePaused = false;
            MusicManager.Instance.UnPauseMusic();
        }

        public void ToMainMenu()
        {
            PhotonNetwork.LeaveRoom();
        }

        public override void OnLeftRoom()
        {
            SceneManager.LoadScene("Main Menu Scene");
            Time.timeScale = 1;
            GameManager.Instance.IsGamePaused = false;
            MusicManager.Instance.PlayMenuMusic();
        }
    }
}
