using UnityEngine;
using PVZ_MVS.Scripts.Managers;
using PVZ_MVS.Scripts.Data;
using PVZ_MVS.Scripts.RuntimeContext;

namespace PVZ_MVS.Scripts.Plants
{
    public class WallNut : Plant
    {
        private bool _isInitialized;

        public override void Initialize(PlantRuntimeContext context){
            base.Initialize(context);
            _isInitialized = true;
        }

        private void Update(){
            if(_isInitialized){
                return;
            }
        }

    }
}