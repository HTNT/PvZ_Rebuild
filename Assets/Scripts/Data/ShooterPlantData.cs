using UnityEngine;

namespace PVZ_MVS.Scripts.Data
{
    public abstract class ShooterPlantData : PlantData{
        [SerializeField] private int _damage;
        [SerializeField] private float _attackRange;
        [SerializeField] private float _attackCooldown;
        [SerializeField] private GameObject _projectilePrefab;

        public int Damage => _damage;
        public float AttackRange => _attackRange;
        public float AttackCooldown => _attackCooldown;
        public GameObject ProjectilePrefab => _projectilePrefab;
    }
}