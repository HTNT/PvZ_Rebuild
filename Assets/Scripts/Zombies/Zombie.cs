using UnityEngine;
using PVZ_MVS.Scripts.Data;
using PVZ_MVS.Scripts.Interfaces;
using PVZ_MVS.Scripts.Managers;

namespace PVZ_MVS.Scripts.Zombies
{
    public abstract class Zombie : MonoBehaviour, IDamageable{
        [SerializeField] private ZombieManager _zombieManager;
        [SerializeField] private ZombieData _data;
        [SerializeField, Min(0)] private int _lane;

        private int _currentHp;

        public ZombieManager Manager => _zombieManager;
        public ZombieData Data => _data;
        public int Lane => _lane;
        public int CurrentHp => _currentHp;

        public virtual void Initialize(){
            if(_data == null){
                Debug.LogError($"{name} chua duoc gan zombiedata.");
                return;
            }
            _currentHp = _data.MaxHp;
        }

        protected virtual void Start(){
            if (_zombieManager == null){
                    Debug.LogError($"{name} chua duoc gan ZombieManager.");
                    return;
                }

                _zombieManager.RegisterZombie(this, Lane);
        }

        protected virtual void OnDestroy(){
            if (_zombieManager != null){
                _zombieManager.UnregisterZombie(this, Lane);
            }
        }

        public virtual void TakeDamage(int damage){
            _currentHp -= damage;
            if(_currentHp <=0){
                Die();
            }
        }

        protected virtual void Die(){
            Destroy(gameObject);
        }
    }
}