using UnityEngine;
using PVZ_MVS.Scripts.Data;
using PVZ_MVS.Scripts.Interfaces;
namespace PVZ_MVS.Scripts.Zombies
{
    public abstract class Zombie : MonoBehaviour, IDamageable{
        [SerializeField] private ZombieData _data;
        private int _currentHp;

        public ZombieData Data => _data;
        public int CurrentHp => _currentHp;

        public virtual void Initialize(){
            if(_data == null){
                Debug.LogError($"{name} chua duoc gan zombiedata.");
                return;
            }
            _currentHp = _data.MaxHp;
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