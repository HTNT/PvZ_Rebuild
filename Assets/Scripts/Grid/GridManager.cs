using UnityEngine;
namespace PVZ_MVS.Scripts.Grid
{
    public class GridManager : MonoBehaviour{
        private Grid _grid;

        private void Awake(){
            _grid = new Grid(5, 9, 1f, Vector3.zero);
            Debug.Log("Grid created");
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
        }
    }
}