using UnityEngine;

namespace PVZ_MVS.Scripts.Plant;
{
    public abstract class Plants : MonoBehaviour{
        [SerializeField] private PlantData _data;
        private int _currentHp;

        public virtual void Initialize(){

        }

        public virtual void TakeDamage(int damage){

        }

        protected virtual void Die(){

        }

    }
}