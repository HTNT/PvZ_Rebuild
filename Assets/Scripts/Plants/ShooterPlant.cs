using UnityEngine;
using PVZ_MVS.Scripts.Data;

namespace PVZ_MVS.Scripts.Plants
{
    public abstract class ShooterPlant : Plant{
        // Runtime State
        //private Zombie _currentTarget;
        private float _attackTimer;

        // Cached Data
        protected ShooterPlantData ShooterData => (ShooterPlantData)Data;

        // Unity Callback
        protected virtual void Update()
        {

        }

        // Core Logic
        protected virtual void FindTarget()
        {

        }

        protected virtual void HandleAttack()
        {

        }

        // Implemented by subclasses
        protected abstract void Shoot();
    }
}