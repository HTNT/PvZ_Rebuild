using UnityEngine;
using TMPro;
using PVZ_MVS.Scripts.Managers;
namespace PVZ_MVS.Scripts.UI
{
    public class SunDisplay : MonoBehaviour
    {
        [SerializeField] SunManager _sunManager;
        [SerializeField] TMP_Text _text;

        public void Update(){
            if(_sunManager == null || _text == null){
                return;
            }
            _text.text = _sunManager.CurrentSun.ToString();
        }
    }
}