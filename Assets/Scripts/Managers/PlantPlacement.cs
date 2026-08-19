using PVZ_MVS.Scripts.Data;
using PVZ_MVS.Scripts.Grid;
using PVZ_MVS.Scripts.Plants;
using PVZ_MVS.Scripts.RuntimeContext;
using UnityEngine;

namespace PVZ_MVS.Scripts.Managers
{
    public class PlantPlacement : MonoBehaviour
    {
        [SerializeField] private GridManager _gridManager;
        [SerializeField] private ZombieManager _zombieManager;
        [SerializeField] private SunManager _sunManager;

        private PlantData _selectedPlantData;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                TryPlaceSelectedPlant();
            }
        }

        public void SelectPlant(PlantData plantData)
        {
            _selectedPlantData = plantData;
        }

        private void TryPlaceSelectedPlant()
        {
            if (_selectedPlantData == null
                || _gridManager == null
                || _zombieManager == null
                || _sunManager == null
                || Camera.main == null)
            {
                return;
            }

            GameObject prefab = _selectedPlantData.Prefab;

            if (prefab == null || prefab.GetComponent<Plant>() == null)
            {
                Debug.LogError($"{_selectedPlantData.PlantName} chua co Plant prefab hop le.");
                return;
            }

            Vector3 mousePosition =
                Camera.main.ScreenToWorldPoint(Input.mousePosition);

            mousePosition.z = 0f;

            Vector2Int cellPosition =
                _gridManager.Grid.GetCellPosition(mousePosition);

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

            if (!_sunManager.TrySpendSun(_selectedPlantData.Cost))
            {
                Debug.Log("Khong du sun.");
                return;
            }

            Plant plant = Instantiate(
                prefab,
                cell.WorldPosition,
                Quaternion.identity).GetComponent<Plant>();

            PlantRuntimeContext context = new PlantRuntimeContext(
                _zombieManager,
                _sunManager,
                row);

            plant.Initialize(context);
            cell.SetPlant(plant);
        }
    }
}