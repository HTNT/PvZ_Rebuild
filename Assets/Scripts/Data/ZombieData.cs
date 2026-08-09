using UnityEngine;

namespace PVZ_MVS.Scripts.Data
{
    public abstract class ZombieData : ScriptableObject
    {
        [SerializeField] private string _zombieName;
        [SerializeField] private int _maxHp;
        [SerializeField] private int _damage;
        [SerializeField] private float _attackCooldown;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private Sprite _icon;
        [SerializeField] private GameObject _prefab;
        [SerializeField, TextArea] private string _description;

        public string ZombieName => _zombieName;
        public int MaxHp => _maxHp;
        public int Damage => _damage;
        public float AttackCooldown => _attackCooldown;
        public float MoveSpeed => _moveSpeed;
        public Sprite Icon => _icon;
        public GameObject Prefab => _prefab;
        public string Description => _description;
    }
}