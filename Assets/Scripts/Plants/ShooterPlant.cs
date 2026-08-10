using UnityEngine;
using PVZ_MVS.Scripts.Data;
using PVZ_MVS.Scripts.Managers;

namespace PVZ_MVS.Scripts.Plants
{
    public abstract class ShooterPlant : Plant{
        // Runtime State
        private Transform _currentTarget;
        private float _attackTimer;
        private bool _isInitialized;
        private bool _hasTargetInLane;

        [SerializeField] private ZombieManager _zombieManager;
        [SerializeField, Min(0)] private int _lane;

        protected ShooterPlantData ShooterData => (ShooterPlantData) Data;
        protected bool HasTargetInLane => _hasTargetInLane;

        // Unity Callback
        protected virtual void Update(){
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
