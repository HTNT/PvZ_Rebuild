using System.Collections.Generic;
using PVZ_MVS.Scripts.Zombies;
using UnityEngine;

namespace PVZ_MVS.Scripts.Managers
{
    public class ZombieManager : MonoBehaviour{
        [SerializeField, Min(1)] private int _laneCount = 5;

        private List<Zombie>[] _zombiesByLane;

        private void Awake(){
            _zombiesByLane = new List<Zombie>[_laneCount];

            for (int lane = 0; lane < _laneCount; lane++){
                _zombiesByLane[lane] = new List<Zombie>();
            }
        }

        public void RegisterZombie(Zombie zombie, int lane){
            if (!IsValidLane(lane) || zombie == null){
                return;
            }

            List<Zombie> zombies = _zombiesByLane[lane];
            if (!zombies.Contains(zombie)){
                zombies.Add(zombie);
            }
        }

        public void UnregisterZombie(Zombie zombie, int lane){
            if (!IsValidLane(lane) || zombie == null){
                return;
            }

            _zombiesByLane[lane].Remove(zombie);
        }

        public bool HasZombieInLane(int lane){
            if (!IsValidLane(lane)){
                return false;
            }

            List<Zombie> zombieLane = _zombiesByLane[lane];

            for (int i = zombieLane.Count - 1; i >= 0; i--){
                if (zombieLane[i] == null){
                    zombieLane.RemoveAt(i);
                }
            }

            return zombieLane.Count > 0;
        }

        public Zombie GetFirstZombieInLane(int lane){
            if (!IsValidLane(lane)){
                return null;
            }

            List<Zombie> zombies = _zombiesByLane[lane];
            Zombie firstZombie = null;
            float closestX = float.MaxValue;

            for (int index = zombies.Count - 1; index >= 0; index--){
                Zombie zombie = zombies[index];
                if (zombie == null){
                    zombies.RemoveAt(index);
                    continue;
                }

                if (zombie.transform.position.x < closestX){
                    closestX = zombie.transform.position.x;
                    firstZombie = zombie;
                }
            }

            return firstZombie;
        }

        private bool IsValidLane(int lane){
            return lane >= 0 && lane < _laneCount;
        }
    }
}
