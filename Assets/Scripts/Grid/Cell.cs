using UnityEngine;

namespace PVZ_MVS.Scripts.Grid
{
    public class Cell{
        public int Row{ get; }
        public int Column{ get; }
        public Vector3 WorldPosition{ get; }
        public Cell(int row, int column, Vector3 worldPosition){
            Row = row;
            Column = column;
            WorldPosition = worldPosition;
        }
    }
}