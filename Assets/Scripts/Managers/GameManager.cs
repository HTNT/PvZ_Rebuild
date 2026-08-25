using System;
using UnityEngine;

namespace PVZ_MVS.Scripts.Managers
{
    public class GameManager : MonoBehaviour
    {
        private enum GameState
        {
            Preparing,
            Playing,
            Won,
            Lost
        }

        [SerializeField] private WaveManager _waveManager;

        private GameState _state = GameState.Preparing;

        public event Action OnGameWon;
        public event Action OnGameLost;
        private void OnEnable()
        {
            _waveManager.OnAllWavesCompleted += HandleVictory;
        }

        private void OnDisable()
        {
            _waveManager.OnAllWavesCompleted -= HandleVictory;
        }

        private void Start()
        {
            Time.timeScale = 1f;
            _state = GameState.Preparing;
        }

        public void StartGame()
        {
            if (_state != GameState.Preparing)
            {
                return;
            }

            _state = GameState.Playing;
            _waveManager.StartWaves();
        }

        private void HandleVictory()
        {
            if (_state != GameState.Playing)
            {
                return;
            }

            _state = GameState.Won;
            Time.timeScale = 0f;
            OnGameWon?.Invoke();
            Debug.Log("You Win");
        }
        public void GameOver()
        {
            if (_state != GameState.Playing)
            {
                return;
            }

            _state = GameState.Lost;
            Time.timeScale = 0f;
            OnGameLost?.Invoke();

            Debug.Log("Game Over");
        }
    }
}