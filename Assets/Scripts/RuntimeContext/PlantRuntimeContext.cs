using UnityEngine;
using PVZ_MVS.Scripts.Managers;


namespace PVZ_MVS.Scripts.RuntimeContext
{
    public class PlantRuntimeContext
    {
        public ZombieManager ZombieManager { get; }
        public SunManager SunManager { get; }
        public int Lane { get; }

        public PlantRuntimeContext(ZombieManager zombieManager, SunManager sunManager, int lane)
        {
            ZombieManager = zombieManager;
            SunManager = sunManager;
            Lane = lane;
        }
    }
}