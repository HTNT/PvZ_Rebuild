using UnityEngine;
namespace PVZ_MVS.Scripts.Grid
{
    public class GridManager : MonoBehaviour{
        [SerializeField, Range(0f, 1f)] private float _screenWidthPercent = 0.7f;
        [SerializeField, Range(0f, 1f)] private float _screenHeightPercent = 0.9f;

        private Grid _grid;
        private Vector2Int _hoverCell = new Vector2Int(-1, -1);

        public Grid Grid => _grid;

        private void Awake(){
            CreateGrid();
            Debug.Log("Grid created");
        }

        private void CreateGrid(){
            Camera mainCamera = Camera.main;

            if (mainCamera == null){
                Debug.LogError("GridManager needs a camera tagged MainCamera.");
                return;
            }

            float gridWidth = mainCamera.orthographicSize * 2f
                * mainCamera.aspect * _screenWidthPercent;
            float gridHeight = mainCamera.orthographicSize * 2f
                * _screenHeightPercent;
            Vector3 gridOrigin = mainCamera.transform.position
                - new Vector3(gridWidth * 0.5f, gridHeight * 0.5f, 0f);
            gridOrigin.z = 0f;

            _grid = new Grid(
                5,
                9,
                gridWidth / 9f,
                gridHeight / 5f,
                gridOrigin
            );
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
                else{
                    _hoverCell = new Vector2Int(-1, -1);
                }
            }
        }

        private void OnDrawGizmos(){
            if (_grid == null)
            {
                CreateGrid();

                if (_grid == null){
                    return;
                }
            }

            Gizmos.color = Color.green;

            for (int row = 0; row < _grid.Rows; row++)
            {
                for (int column = 0; column < _grid.Columns; column++)
                {
                    Cell cell = _grid.GetCell(row, column);

                    Gizmos.DrawWireCube(
                        cell.WorldPosition,
                        new Vector3(_grid.CellWidth, _grid.CellHeight, 1f)
                    );
                }
            }

            if (_grid.IsValidCell(_hoverCell.y, _hoverCell.x)){
                Gizmos.color = Color.red;

                Vector3 position =
                    _grid.GetWorldPosition(_hoverCell.y, _hoverCell.x);

                Gizmos.DrawCube(
                    position,
                    new Vector3(_grid.CellWidth, _grid.CellHeight, 0.05f));
            }
        }
    }
}
