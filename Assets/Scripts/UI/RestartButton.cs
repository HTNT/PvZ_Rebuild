using UnityEngine;
using UnityEngine.SceneManagement;

namespace PVZ_MVS.Scripts.UI
{
    public class RestartButton : MonoBehaviour
    {
        public void RestartGame()
        {
            Time.timeScale = 1f;

            SceneManager.LoadScene(
                SceneManager.GetActiveScene().buildIndex);
        }
    }
}