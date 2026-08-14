using UnityEngine;

namespace PVZ_MVS.Scripts.Data
{
    public class SunFlowerData : PlantData
    {
        [SerializeField] private int _sunAmount;
        [SerializeField] private float _sunCooldown;

        public int SunAmount => _sunAmount;
        public float SunCooldown => _sunCooldown;
    }
}