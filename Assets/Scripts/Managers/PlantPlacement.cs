using UnityEngine;
using PVZ_MVS.Scripts.Grid;
using PVZ_MVS.Scripts.Managers;
using PVZ_MVS.Scripts.Plants;

namespace PVZ_MVS.Scripts.Managers
{
    public class PlantPlacement : MonoBehaviour
    {
        [SerializeField] private GridManager _gridManager;
        [SerializeField] private ZombieManager _zombieManager;
        [SerializeField] private Peashooter _peashooterPrefab;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                TryPlacePeashooter();
            }
        }

        private void TryPlacePeashooter()
        {
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
                Debug.Log("Da co plant");
                return;
            }

            Peashooter plant = Instantiate(
                _peashooterPrefab,
                cell.WorldPosition,
                Quaternion.identity);

            plant.Initialize(_zombieManager, row);
            cell.SetPlant(plant);
        }
    }
}