using PVZ_MVS.Scripts.Data;
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

        public void ShowPlant(PlantData plantData)
        {
            if (plantData == null)
            {
                Clear();
                return;
            }

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
        }
    }
}