using UnityEngine;
using PVZ_MVS.Scripts.Zombies;

namespace PVZ_MVS.Scripts.Managers
{
    public class ZombieSpawner : MonoBehaviour
    {
        [SerializeField] private ZombieManager _zombieManager;
        [SerializeField] private Zombie _basicZombiePrefab;
        [SerializeField] private Transform[] _spawnPoints;

        [SerializeField, Min(0f)] private float _spawnInterval = 3f;

        private float _spawnTimer;

        private void Update(){
            // if (Input.GetKeyDown(KeyCode.Space))
            // {
            //     SpawnZombie(0);
            // }
            _spawnTimer -= Time.deltaTime;

            if (_spawnTimer > 0f)
            {
                return;
            }

            int randomLane = Random.Range(0, _spawnPoints.Length);

            SpawnZombie(randomLane);

            _spawnTimer = _spawnInterval;
        }

        private void SpawnZombie(int lane){
            if (!IsValidLane(lane))
            {
                Debug.LogError($"Lane {lane} khong hop le.");
                return;
            }

            Zombie zombie = CreateZombie(lane);

            if (zombie == null)
            {
                return;
            }

            zombie.Initialize(_zombieManager, lane);
        } 

        private bool IsValidLane(int lane){
            return lane >= 0
                && lane < _spawnPoints.Length
                && _spawnPoints[lane] != null;
        }

        private Zombie CreateZombie(int lane){
            if (_basicZombiePrefab == null)
            {
                Debug.LogError("Chua gan Basic Zombie Prefab.");
                return null;
            }

            Transform spawnPoint = _spawnPoints[lane];

            return Instantiate(
                _basicZombiePrefab,
                spawnPoint.position,
                Quaternion.identity);
        }
    }
}