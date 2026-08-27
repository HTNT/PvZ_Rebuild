using PVZ_MVS.Scripts.Data;
using PVZ_MVS.Scripts.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PVZ_MVS.Scripts.UI
{
    public class SelectedPlantSlotUI : MonoBehaviour
    {
        [SerializeField] private Image _iconImage;
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _costText;
        [SerializeField] private GameObject _emptyVisual;
        [SerializeField] private GameObject _filledVisual;
        [SerializeField] private Button _button;

        private PlantData _plantData;
        private PlantPlacement _plantPlacement;

        private void Awake()
        {
            if (_button != null)
            {
                _button.onClick.AddListener(SelectPlant);
            }
        }

        private void OnDestroy()
        {
            if (_button != null)
            {
                _button.onClick.RemoveListener(SelectPlant);
            }
        }

        public void ShowPreview(PlantData plantData)
        {
            ShowPlant(plantData);
            _plantPlacement = null;

            if (_button != null)
            {
                _button.interactable = false;
            }
        }

        public void LockForGameplay(
            PlantData plantData,
            PlantPlacement plantPlacement)
        {
            ShowPlant(plantData);
            _plantPlacement = plantPlacement;

            if (_button != null)
            {
                _button.interactable = _plantData != null
                    && _plantPlacement != null;
            }
        }

        public void ShowPlant(PlantData plantData)
        {
            if (plantData == null)
            {
                Clear();
                return;
            }

            _plantData = plantData;

            if (_emptyVisual != null)
            {
                _emptyVisual.SetActive(false);
            }

            if (_filledVisual != null)
            {
                _filledVisual.SetActive(true);
            }

            if (_iconImage != null)
            {
                _iconImage.sprite = plantData.Icon;
            }

            if (_nameText != null)
            {
                _nameText.text = plantData.PlantName;
            }
            if (_costText != null)
            {
                _costText.text = plantData.Cost.ToString();
            }
        }

        public void Clear()
        {
            _plantData = null;
            _plantPlacement = null;

            if (_emptyVisual != null)
            {
                _emptyVisual.SetActive(true);
            }

            if (_filledVisual != null)
            {
                _filledVisual.SetActive(false);
            }

            if (_iconImage != null)
            {
                _iconImage.sprite = null;
            }

            if (_nameText != null)
            {
                _nameText.text = string.Empty;
            }

            if (_costText != null)
            {
                _costText.text = string.Empty;
            }

            if (_button != null)
            {
                _button.interactable = false;
            }
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
