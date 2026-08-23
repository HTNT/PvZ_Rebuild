using PVZ_MVS.Scripts.Zombies;
using UnityEngine;

namespace PVZ_MVS.Scripts.Managers
{
    public class GameOverZone : MonoBehaviour
    {
        [SerializeField] private GameManager _gameManager;

        private void OnTriggerEnter2D(Collider2D other)
        {
            Zombie zombie = other.GetComponentInParent<Zombie>();

            if (zombie == null)
            {
                return;
            }

            _gameManager.GameOver();
        }
    }
}