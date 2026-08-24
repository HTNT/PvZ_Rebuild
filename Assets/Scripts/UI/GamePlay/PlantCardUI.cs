using PVZ_MVS.Scripts.Data;
using PVZ_MVS.Scripts.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PVZ_MVS.Scripts.UI
{
    public class PlantCardUI : MonoBehaviour
    {
        [SerializeField] private PlantData _plantData;
        [SerializeField] private PlantPlacement _plantPlacement;

        [Header("UI")]
        [SerializeField] private Image _iconImage;
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _costText;
        [SerializeField] private Button _button;

        private void Awake()
        {
            if (_button == null)
            {
                Debug.LogError($"{name} chua duoc gan Button.");
                return;
            }

            RefreshView();
            _button.onClick.AddListener(SelectPlant);
        }

        private void OnDestroy()
        {
            if (_button != null)
            {
                _button.onClick.RemoveListener(SelectPlant);
            }
        }

        private void RefreshView()
        {
            if (_plantData == null)
            {
                return;
            }

            _iconImage.sprite = _plantData.Icon;
            _nameText.text = _plantData.PlantName;
            _costText.text = _plantData.Cost.ToString();
        }

        private void SelectPlant()
        {
            if (_plantData == null || _plantPlacement == null)
            {
                return;
            }

            _plantPlacement.SelectPlant(_plantData);
        }
    }
}