using UnityEngine;

namespace PVZ_MVS.Scripts.Grid
{
    public class Grid{
        public int Rows{ get; }
        public int Columns{ get; }

        public float CellWidth{ get; }
        public float CellHeight{ get; }

        public Vector3 Origin { get; }

        private Cell[,] _cells;
        
        public Grid(int rows, int columns, float cellWidth, float cellHeight, Vector3 origin){
            Rows = rows;
            Columns = columns;
            CellWidth = cellWidth;
            CellHeight = cellHeight;
            Origin = origin;

            _cells = new Cell[Rows, Columns];

            for (int row = 0; row < Rows; row++){
                for (int column = 0; column < Columns; column++){
                        float x = Origin.x + (column + 0.5f) * CellWidth;
                        float y = Origin.y + (row + 0.5f) * CellHeight;

                        Vector3 worldPosition = new Vector3(x, y, 0);

                        _cells[row, column] = new Cell(row, column, worldPosition);
                }
            }
        }

        public Cell GetCell(int row, int column){
            return _cells[row, column];
        }

        public Vector2Int GetCellPosition(Vector3 worldPosition){
            Vector3 localPosition = worldPosition - Origin;

            int column = Mathf.FloorToInt(localPosition.x / CellWidth);
            int row = Mathf.FloorToInt(localPosition.y / CellHeight);

            return new Vector2Int(column, row);
        }

        public bool IsValidCell(int row, int column){
            return (row >= 0 && row < Rows) && (column >=0 && column < Columns);
        }

        public Vector3 GetWorldPosition(int row, int column){
            return _cells[row, column].WorldPosition;
        }
    }
}
