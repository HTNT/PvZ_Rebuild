using UnityEngine;
using PVZ_MVS.Scripts.Data;
using PVZ_MVS.Scripts.Managers;
using PVZ_MVS.Scripts.RuntimeContext;

namespace PVZ_MVS.Scripts.Plants
{
    public abstract class ShooterPlant : Plant{
        // Runtime State
        private Transform _currentTarget;
        private float _attackTimer;
        private bool _isInitialized;
        private bool _hasTargetInLane;

        private ZombieManager _zombieManager;
        private int _lane;

        protected ShooterPlantData ShooterData => (ShooterPlantData) Data;
        protected bool HasTargetInLane => _hasTargetInLane;

        public override void Initialize(PlantRuntimeContext context){
            if (context.ZombieManager == null){
                Debug.LogError($"{name} chua duoc gan ZombieManager.");
                return;
            }

            base.Initialize(context);

            _zombieManager = context.ZombieManager;
            _lane = context.Lane;
            _attackTimer = 0f;
            _isInitialized = true;
        }

        // Unity Callback
        protected virtual void Update(){
            if (!_isInitialized)
            {
                return;
            }
            _attackTimer = Mathf.Max(0f, _attackTimer - Time.deltaTime);

            FindTarget();
            HandleAttack();
        }

        // Core Logic
        protected virtual void FindTarget(){
            if (_zombieManager == null){
                _hasTargetInLane = false;
                return;
            }

            _hasTargetInLane = _zombieManager.HasZombieInLane(_lane);
        }

        protected virtual void HandleAttack(){
            if (!HasTargetInLane || _attackTimer > 0f){
                return;
            }

            Shoot();
            _attackTimer = ShooterData.AttackCooldown;
        }

        // Implemented by subclasses
        protected abstract void Shoot();

    }
}
