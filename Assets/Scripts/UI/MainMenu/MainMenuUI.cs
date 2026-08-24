using UnityEngine;
using UnityEngine.SceneManagement;

namespace PVZ_MVS.Scripts.UI
{
    public class MainMenuUI : MonoBehaviour
    {
        [SerializeField] private string _gameplaySceneName =
            "MainGamePlay";

        public void PlayGame()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(_gameplaySceneName);
        }

        public void QuitGame()
        {
#if UNITY_EDITOR
            Debug.Log("Quit Game");
#else
            Application.Quit();
#endif
        }
    }
}