using UnityEngine;
using PVZ_MVS.Scripts.Managers;
using PVZ_MVS.Scripts.Data;

namespace PVZ_MVS.Scripts.Plants
{
    public class SunFlower : Plant
    {
        [SerializeField]private SunManager _sunManager;
        [SerializeField]private float _sunTimer;
        private bool _isInitialized;

        protected SunFlowerData SunFlowerData => (SunFlowerData)Data;

        public void Initialize(SunManager sunManager){
            if (sunManager == null)
            {
                Debug.LogError($"{name} chua duoc gan SunManager.");
                return;
            }
            Debug.Log("Sun da vao tran");
            base.Initialize();

            _sunManager = sunManager;
            _sunTimer = SunFlowerData.SunCooldown;
            _isInitialized = true;
        }
        private void Start(){
            Initialize(_sunManager);
        }
        private void Update(){
            if(!_isInitialized){
                return;
            }
            _sunTimer -= Time.deltaTime;

            if(_sunTimer > 0f){
                return;
            }
            GenerateSun();

        }

        public void GenerateSun(){
            _sunManager.AddSun(SunFlowerData.SunAmount);
            _sunTimer = SunFlowerData.SunCooldown;
        }


    }
}