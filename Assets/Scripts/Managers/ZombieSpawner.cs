using PVZ_MVS.Scripts.Data;
using PVZ_MVS.Scripts.Grid;
using PVZ_MVS.Scripts.Zombies;
using UnityEngine;
using GridData = PVZ_MVS.Scripts.Grid.Grid;

namespace PVZ_MVS.Scripts.Managers
{
    public class ZombieSpawner : MonoBehaviour
    {
        [SerializeField] private ZombieManager _zombieManager;
        [SerializeField] private GridManager _gridManager;
        [SerializeField, Min(0f)] private float _spawnOffset = 0.5f;

        private Transform[] _spawnPoints;
        private bool _areSpawnPointsCreated;

        private void Awake(){
            if (_gridManager == null){
                _gridManager = FindAnyObjectByType<GridManager>();
            }
        }

        private void Start(){
            CreateSpawnPoints();
        }

        public bool SpawnRandomZombie(ZombieData zombieData){
            if (!EnsureSpawnPoints()){
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

        private bool EnsureSpawnPoints(){
            return _areSpawnPointsCreated || CreateSpawnPoints();
        }

        private bool CreateSpawnPoints(){
            if (_areSpawnPointsCreated){
                return true;
            }

            if (_gridManager == null || _gridManager.Grid == null){
                Debug.LogError("ZombieSpawner can GridManager da duoc khoi tao.");
                return false;
            }

            GridData grid = _gridManager.Grid;
            _spawnPoints = new Transform[grid.Rows];

            GameObject container = new GameObject("Generated Spawn Points");
            container.transform.SetParent(transform);

            float spawnX = grid.Origin.x + grid.Columns * grid.CellWidth + _spawnOffset;

            for (int lane = 0; lane < grid.Rows; lane++){
                GameObject spawnPoint = new GameObject($"Spawn Point Lane {lane}");
                spawnPoint.transform.SetParent(container.transform);
                spawnPoint.transform.position = new Vector3(
                    spawnX,
                    grid.GetWorldPosition(lane, 0).y,
                    0f
                );

                _spawnPoints[lane] = spawnPoint.transform;
            }

            _areSpawnPointsCreated = true;
            return true;
        }

        private bool IsValidLane(int lane){
            return lane >= 0
                && lane < _spawnPoints.Length
                && _spawnPoints[lane] != null;
        }

    }
}
