using UnityEngine;
namespace PVZ_MVS.Scripts.Grid
{
    public class GridManager : MonoBehaviour{
        private Grid _grid;
        private Vector2Int _hoverCell = new Vector2Int(-1, -1);

        private void Awake(){
            _grid = new Grid(5, 9, 1f, Vector3.zero);
            Debug.Log("Grid created");
        }

        private void Update(){
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mouseWorldPosition =
                    Camera.main.ScreenToWorldPoint(Input.mousePosition);

                mouseWorldPosition.z = 0;

                Vector2Int cellPosition =
                    _grid.GetCellPosition(mouseWorldPosition);

                Debug.Log($"Row: {cellPosition.y}, Column: {cellPosition.x}");

                if (_grid.IsValidCell(cellPosition.y, cellPosition.x)){
                    _hoverCell = cellPosition;
                }
            }
        }

        private void OnDrawGizmos(){
            if (_grid == null)
            {
                _grid = new Grid(5, 9, 1f, Vector3.zero);
            }

            Gizmos.color = Color.green;

            for (int row = 0; row < _grid.Rows; row++)
            {
                for (int column = 0; column < _grid.Columns; column++)
                {
                    Cell cell = _grid.GetCell(row, column);

                    Gizmos.DrawWireCube(
                        cell.WorldPosition,
                        Vector3.one * _grid.CellSize
                    );
                }
            }

            if (_grid.IsValidCell(_hoverCell.y, _hoverCell.x)){
                Gizmos.color = Color.red;

                Vector3 position =
                    _grid.GetWorldPosition(_hoverCell.y, _hoverCell.x);

                Gizmos.DrawCube(
                    position,
                    new Vector3(_grid.CellSize, _grid.CellSize, 0.05f));
            }
        }
    }
}