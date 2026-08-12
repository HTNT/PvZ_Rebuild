using UnityEngine;
using PVZ_MVS.Scripts.Interfaces;
namespace PVZ_MVS.Scripts.Projectiles
{
    public abstract class Projectile : MonoBehaviour
    {
        [SerializeField, Min(0f)] private float _lifeTime = 5f;
        [SerializeField, Min(0f)] private float _speed = 5f;
        private int _damaged;

        public float Speed => _speed;
        public int Damage => _damaged;

        public virtual void Initialize(int damage){
                _damaged = damage;
            }
        private void Start(){
            Destroy(gameObject, _lifeTime);
        }
        protected virtual void Update(){
            transform.position += Vector3.right * _speed * Time.deltaTime;
        }

        protected virtual void OnTriggerEnter2D(Collider2D collider){
            IDamageable damageable = collider.GetComponent<IDamageable>();

            if(!collider.CompareTag("Zombie")){
                return;
            }
            if (damageable == null)
            {
                return;
            }

            damageable.TakeDamage(_damaged);
            Destroy(gameObject);
        }
    }
}