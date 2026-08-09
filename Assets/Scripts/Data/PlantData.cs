using UnityEngine;

namespace PVZ_MVS.Scripts.Data
{
    public abstract class PlantData : ScriptableObject{
        [SerializeField] private string _plantName;
        [SerializeField] private int _cost;
        [SerializeField] private int _maxHp;
        [SerializeField] private Sprite _icon;
        [SerializeField] private GameObject _prefab;
        [SerializeField, TextArea] private string _description;

        public string PlantName => _plantName;
        public int Cost => _cost;
        public int MaxHp => _maxHp;
        public Sprite Icon => _icon;
        public GameObject Prefab => _prefab;
        public string Description => _description;

    }
}