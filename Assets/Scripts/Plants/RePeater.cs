using UnityEngine;
using PVZ_MVS.Scripts.Projectiles;

namespace PVZ_MVS.Scripts.Plants
{
    public class RePeater : ShooterPlant
    {
        [SerializeField] private Transform _firePoint;

        protected override void Shoot()
        {
            RePeaterShoot();
            StartCoroutine(
                DelayedAction(0.2f, RePeaterShoot)
            );  
        }
        private void RePeaterShoot()
        {
            GameObject projectileObject = Instantiate(
                ShooterData.ProjectilePrefab,
                _firePoint.position,
                Quaternion.identity);

            Projectile projectile = projectileObject.GetComponent<Projectile>();

            if (projectile == null)
            {
                Debug.LogError("Projectile prefab cần có component Projectile.");
                Destroy(projectileObject);
                return;
            }

            projectile.Initialize(ShooterData.Damage);
        }
        
    }
}