using System;
using PVZ_MVS.Scripts.Data;
using UnityEngine;

namespace PVZ_MVS.Scripts.Managers
{
    public class WaveManager : MonoBehaviour
    {
        private enum WaveState
        {
            Idle,
            Spawning,
            WaitingForClear,
            Complete
        }

        [SerializeField] private ZombieSpawner _zombieSpawner;
        [SerializeField] private ZombieManager _zombieManager;
        [SerializeField] private WaveData[] _waves;

        private WaveState _state = WaveState.Idle;
        private int _currentWaveIndex = -1;
        private int _currentEntryIndex;
        private int _spawnedInCurrentEntry;
        private float _spawnTimer;

        public int CurrentWaveNumber => _currentWaveIndex + 1;
        public bool IsComplete => _state == WaveState.Complete;

        public event Action<int> OnWaveStarted;
        public event Action<int> OnWaveCompleted;
        public event Action OnAllWavesCompleted;

        // private void Start(){
        //     StartWaves();
        // }

        private void Update(){
            if (_state == WaveState.Spawning){
                HandleSpawning();
                return;
            }

            if (_state == WaveState.WaitingForClear){
                HandleWaitingForClear();
            }
        }

        public void StartWaves(){
            if (_state != WaveState.Idle){
                return;
            }

            if (_zombieSpawner == null || _zombieManager == null || _waves == null){
                Debug.LogError("WaveManager chua duoc cau hinh day du.");
                return;
            }

            BeginNextWave();
        }

        private void HandleSpawning(){
            _spawnTimer -= Time.deltaTime;

            if (_spawnTimer > 0f){
                return;
            }

            WaveSpawnEntry entry =
                _waves[_currentWaveIndex].SpawnEntries[_currentEntryIndex];

            if (entry.ZombieDatas == null || entry.ZombieDatas.Length == 0)
            {
                Debug.LogError("WaveSpawnEntry chua co ZombieData.");
                _state = WaveState.Idle;
                return;
            }
            ZombieData randomZombieData = entry.ZombieDatas[
            UnityEngine.Random.Range(0, entry.ZombieDatas.Length)];


            if (!_zombieSpawner.SpawnRandomZombie(randomZombieData))
            {
                Debug.LogError("Spawn wave that bai.");
                _state = WaveState.Idle;
                return;
            }

            _spawnedInCurrentEntry++;

            if (_spawnedInCurrentEntry < entry.Count){
                _spawnTimer = entry.SpawnInterval;
                return;
            }

            _currentEntryIndex++;
            _spawnedInCurrentEntry = 0;
            _spawnTimer = 0f;

            if (_currentEntryIndex >= _waves[_currentWaveIndex].SpawnEntries.Length){
                _state = WaveState.WaitingForClear;
            }
        }

        private void HandleWaitingForClear(){
            if (_zombieManager.ActiveZombieCount > 0){
                return;
            }

            OnWaveCompleted?.Invoke(CurrentWaveNumber);
            BeginNextWave();
        }

        private void BeginNextWave(){
            _currentWaveIndex++;

            if (_currentWaveIndex >= _waves.Length){
                _state = WaveState.Complete;
                OnAllWavesCompleted?.Invoke();
                return;
            }

            WaveData wave = _waves[_currentWaveIndex];

            if (wave == null
                || wave.SpawnEntries == null
                || wave.SpawnEntries.Length == 0){
                Debug.LogError($"Wave {CurrentWaveNumber} khong co spawn entry.");
                _state = WaveState.Idle;
                return;
            }

            _currentEntryIndex = 0;
            _spawnedInCurrentEntry = 0;
            _spawnTimer = 0f;
            _state = WaveState.Spawning;

            OnWaveStarted?.Invoke(CurrentWaveNumber);
        }
    }
}
