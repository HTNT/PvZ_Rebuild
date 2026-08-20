using System;
using UnityEngine;

namespace PVZ_MVS.Scripts.Data
{
    [CreateAssetMenu(
        fileName = "WaveData",
        menuName = "PVZ/Waves/Wave Data")]
    public class WaveData : ScriptableObject
    {
        [SerializeField] private WaveSpawnEntry[] _spawnEntries;

        public WaveSpawnEntry[] SpawnEntries => _spawnEntries;
    }

    [Serializable]
    public class WaveSpawnEntry
    {
        [SerializeField] private ZombieData _zombieData;
        [SerializeField, Min(1)] private int _count = 1;
        [SerializeField, Min(0f)] private float _spawnInterval = 1f;

        public ZombieData ZombieData => _zombieData;
        public int Count => _count;
        public float SpawnInterval => _spawnInterval;
    }
}
