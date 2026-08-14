using UnityEngine;

namespace PVZ_MVS.Scripts.Managers
{
    public class SunManager : MonoBehaviour
    {
        [SerializeField, Min(0)] private int _startingSun;
        [SerializeField] private int _currentSun;

        public int CurrentSun => _currentSun;

        private void Awake(){
            _currentSun = _startingSun;
        }

        public void AddSun(int sun){
            if(sun < 0){
                return;
            }
            _currentSun += sun;
        }

        public bool CanAfford(int sun){
            return sun <= _currentSun;
        }

        public bool TrySpendSun(int sun){
            if(!CanAfford(sun)){
                return false;
            }
            _currentSun -= sun;
            return true;
        }

    }
}