using UnityEngine;
using PVZ_MVS.Scripts.Plants;
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

        public Plant OccupyingPlant { private set; get;}
        public bool IsOccupied => OccupyingPlant != null;

        public void SetPlant(Plant plant){
            OccupyingPlant = plant;
        }
        public void ClearPlant(){
            OccupyingPlant = null;
        }
    }
}