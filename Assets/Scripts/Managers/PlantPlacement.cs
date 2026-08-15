using PVZ_MVS.Scripts.Grid;
using PVZ_MVS.Scripts.Plants;
using UnityEngine;

namespace PVZ_MVS.Scripts.Managers
{
    public class PlantPlacement : MonoBehaviour
    {
        [SerializeField] private GridManager _gridManager;
        [SerializeField] private ZombieManager _zombieManager;
        [SerializeField] private SunManager _sunManager;
        [SerializeField] private Peashooter _peashooterPrefab;
        [SerializeField] private SunFlower _sunFlowerPrefab;

        private Plant _selectedPlantPrefab;

        private void Start()
        {
            _selectedPlantPrefab = _peashooterPrefab;
        }

        private void Update()
        {
            HandlePlantSelection();

            if (Input.GetMouseButtonDown(0))
            {
                TryPlaceSelectedPlant();
            }
        }

        private void HandlePlantSelection()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1)
                || Input.GetKeyDown(KeyCode.Keypad1))
            {
                _selectedPlantPrefab = _peashooterPrefab;
                Debug.Log("Da chon Peashooter");
            }

            if (Input.GetKeyDown(KeyCode.Alpha2)
                || Input.GetKeyDown(KeyCode.Keypad2))
            {
                _selectedPlantPrefab = _sunFlowerPrefab;
                Debug.Log("Da chon SunFlower");
            }
        }

        private void TryPlaceSelectedPlant()
        {
            if (_selectedPlantPrefab == null || _gridManager == null || _sunManager == null)
            {
                return;
            }

            Vector3 mouseWorldPosition =
                Camera.main.ScreenToWorldPoint(Input.mousePosition);

            mouseWorldPosition.z = 0f;

            Vector2Int cellPosition =
                _gridManager.Grid.GetCellPosition(mouseWorldPosition);

            int column = cellPosition.x;
            int row = cellPosition.y;

            if (!_gridManager.Grid.IsValidCell(row, column))
            {
                return;
            }

            Cell cell = _gridManager.Grid.GetCell(row, column);

            if (cell.IsOccupied)
            {
                return;
            }

            if (!HasRequiredManager())
            {
                return;
            }

            int cost = _selectedPlantPrefab.Data.Cost;

            if (!_sunManager.TrySpendSun(cost))
            {
                Debug.Log("Khong du sun.");
                return;
            }

            Plant plant = Instantiate(
                _selectedPlantPrefab,
                cell.WorldPosition,
                Quaternion.identity);

            InitializePlant(plant, row);

            cell.SetPlant(plant);
        }

        private bool HasRequiredManager()
        {
            if (_selectedPlantPrefab is ShooterPlant)
            {
                return _zombieManager != null;
            }

            return true;
        }

        private void InitializePlant(Plant plant, int lane)
        {
            if (plant is ShooterPlant shooterPlant)
            {
                shooterPlant.Initialize(_zombieManager, lane);
                return;
            }

            if (plant is SunFlower sunFlower)
            {
                sunFlower.Initialize(_sunManager);
                return;
            }

            plant.Initialize();
        }
    }
}