using PVZ_MVS.Scripts.Managers;
using UnityEngine;

namespace PVZ_MVS.Scripts.UI
{
    public class GameResultUI : MonoBehaviour
    {
        [SerializeField] private GameManager _gameManager;
        [SerializeField] private GameObject _winPanel;
        [SerializeField] private GameObject _losePanel;

        private void Awake()
        {
            _winPanel.SetActive(false);
            _losePanel.SetActive(false);
        }

        private void OnEnable()
        {
            if (_gameManager == null)
            {
                return;
            }

            _gameManager.OnGameWon += ShowWinPanel;
            _gameManager.OnGameLost += ShowLosePanel;
        }

        private void OnDisable()
        {
            if (_gameManager == null)
            {
                return;
            }

            _gameManager.OnGameWon -= ShowWinPanel;
            _gameManager.OnGameLost -= ShowLosePanel;
        }

        private void ShowWinPanel()
        {
            _losePanel.SetActive(false);
            _winPanel.SetActive(true);
        }

        private void ShowLosePanel()
        {
            _winPanel.SetActive(false);
            _losePanel.SetActive(true);
        }
    }
}