using UnityEngine;
using PVZ_MVS.Scripts.Data;
using PVZ_MVS.Scripts.Interfaces;
using PVZ_MVS.Scripts.Managers;
using PVZ_MVS.Scripts.Plants;

namespace PVZ_MVS.Scripts.Zombies
{
    public abstract class Zombie : MonoBehaviour, IDamageable{
        [SerializeField] private ZombieData _data;
        [SerializeField] private int _currentHp;

        private ZombieManager _zombieManager;
        [SerializeField] private int _lane;
        private Plant _currentPlant;
        private IDamageable _currentTarget;
        private float _attackTimer;

        public ZombieManager Manager => _zombieManager;
        public ZombieData Data => _data;
        public int Lane => _lane;
        public int CurrentHp => _currentHp;

        public virtual void Initialize(ZombieManager zombieManager, int lane){
            if (_data == null)
            {
                Debug.LogError($"{name} chua duoc gan ZombieData.");
                return;
            }

            if (zombieManager == null)
            {
                Debug.LogError($"{name} chua duoc gan ZombieManager.");
                return;
            }

            _zombieManager = zombieManager;
            _lane = lane;
            _currentHp = _data.MaxHp;
            _attackTimer = _data.AttackCooldown;

            _zombieManager.RegisterZombie(this, _lane);
        }

        protected virtual void Update(){
            if(_currentPlant == null){
                //Debug.Log("Move");
                Move();
            }else{

                _attackTimer = Mathf.Max(0f, _attackTimer - Time.deltaTime);
                HandleAttack();
            }
        }

        protected virtual void Move(){
            transform.position += Vector3.left * Data.MoveSpeed * Time.deltaTime;
        }

        protected virtual void HandleAttack(){
            if (_currentTarget == null){
                return;
            }
            if(_attackTimer > 0f){
                return;
            }
            Attack();
            _attackTimer = Data.AttackCooldown;
        }

        protected virtual void Attack(){
            if (_currentTarget == null){
                return;
            }
            //Debug.Log("Attack");
            _currentTarget.TakeDamage(Data.Damage);
        }

        protected virtual void FindPlantAhead(){

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

        private void OnTriggerEnter2D(Collider2D collider){
            //Debug.Log($"Trigger: {collider.name}, tag: {collider.tag}");
            if(!collider.CompareTag("Plant")){
                return;
            }
            _currentPlant = collider.GetComponentInParent<Plant>();
            _currentTarget = collider.GetComponentInParent<IDamageable>();
        }

        private void OnTriggerExit2D(Collider2D collider){
            if(!collider.CompareTag("Plant")){
                return;
            }
            IDamageable target = collider.GetComponentInParent<IDamageable>();
            
            if (target == _currentTarget)
            {
                _currentPlant = null;
                _currentTarget = null;
            }
        }
    }
}
