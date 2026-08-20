using PVZ_MVS.Scripts.Data;
using PVZ_MVS.Scripts.Zombies;
using UnityEngine;

namespace PVZ_MVS.Scripts.Managers
{
    public class ZombieSpawner : MonoBehaviour
    {
        [SerializeField] private ZombieManager _zombieManager;
        [SerializeField] private Transform[] _spawnPoints;

        public bool SpawnRandomZombie(ZombieData zombieData){
            if (_spawnPoints == null || _spawnPoints.Length == 0){
                Debug.LogError("Chua gan Spawn Points.");
                return false;
            }

            int lane = Random.Range(0, _spawnPoints.Length);

            return SpawnZombie(zombieData, lane);
        }

        public bool SpawnZombie(ZombieData zombieData, int lane){
            if (zombieData == null || zombieData.Prefab == null){
                Debug.LogError("ZombieData chua co prefab.");
                return false;
            }

            if (_zombieManager == null || !IsValidLane(lane)){
                Debug.LogError($"Khong the spawn zombie o lane {lane}.");
                return false;
            }

            Zombie zombiePrefab = zombieData.Prefab.GetComponent<Zombie>();

            if (zombiePrefab == null){
                Debug.LogError("Zombie prefab can co component Zombie.");
                return false;
            }

            Zombie zombie = Instantiate(
                zombiePrefab,
                _spawnPoints[lane].position,
                Quaternion.identity);

            zombie.Initialize(_zombieManager, lane);

            return true;
        }

        private bool IsValidLane(int lane){
            return lane >= 0
                && lane < _spawnPoints.Length
                && _spawnPoints[lane] != null;
        }

    }
}
