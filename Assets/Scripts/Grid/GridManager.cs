using UnityEngine;
namespace PVZ_MVS.Scripts.Grid
{
    public class GridManager : MonoBehaviour{
        private const int RowCount = 5;
        private const int ColumnCount = 9;

        [SerializeField, Range(0.1f, 1f)] private float _gridHeightPercent = 0.9f;
        [SerializeField, Range(0f, 1f)] private float _bottomPaddingPercent = 0f;
        [SerializeField, Range(0f, 1f)] private float _rightPaddingPercent = 0.11f;

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

            float screenHeight = mainCamera.orthographicSize * 2f;
            float screenWidth = screenHeight * mainCamera.aspect;
            float gridHeight = screenHeight * _gridHeightPercent;

            float cellHeight = gridHeight / RowCount * 1.05f;
            float cellWidth = cellHeight * 3f / 4f * 1.1f * 1.1f * 0.95f * 0.98f;
            float gridWidth = ColumnCount * cellWidth;
            float screenBottom = mainCamera.transform.position.y
                - screenHeight * 0.5f;
            float screenLeft = mainCamera.transform.position.x
                - screenWidth * 0.5f;
            float leftPadding = screenWidth
                - gridWidth
                - screenWidth * _rightPaddingPercent;

            Vector3 gridOrigin = new Vector3(
                screenLeft + leftPadding,
                screenBottom + screenHeight * _bottomPaddingPercent,
                0f
            );

            _grid = new Grid(
                RowCount,
                ColumnCount,
                cellWidth,
                cellHeight,
                gridOrigin
            );
        }

        private void OnValidate(){
            _grid = null;
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
