using UnityEngine;

namespace PVZ_MVS.Scripts.Interfaces
{
    public interface IDamageable
    {
        int CurrentHp { get; }
        void TakeDamage(int damage);
    }
}