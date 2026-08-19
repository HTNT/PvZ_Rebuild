using UnityEngine;
using PVZ_MVS.Scripts.Data;
using PVZ_MVS.Scripts.Interfaces;
using PVZ_MVS.Scripts.RuntimeContext;

namespace PVZ_MVS.Scripts.Plants
{
    public abstract class Plant : MonoBehaviour, IDamageable{
        [SerializeField] private PlantData _data;
        private int _currentHp;

        public PlantData Data => _data;
        public int CurrentHp => _currentHp;

        public virtual void Initialize(PlantRuntimeContext context){
            if(_data == null){
                Debug.LogError($"{name} chua duoc gan plantdata.");
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