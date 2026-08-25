using PVZ_MVS.Scripts.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using PVZ_MVS.Scripts.Managers;

namespace PVZ_MVS.Scripts.UI
{
    public class PlantSelectionCardUI : MonoBehaviour
    {
        [SerializeField] private PlantData _plantData;
        [SerializeField] private PlantSelectionManager _selectionManager;

        [Header("UI")]
        [SerializeField] private Image _iconImage;
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _costText;
        [SerializeField] private Image _backgroundImage;
        [SerializeField] private Button _button;

        [Header("Color")]
        [SerializeField] private Color _normalColor = Color.white;
        [SerializeField] private Color _selectedColor = Color.green;

        public PlantData PlantData => _plantData;

        public void Initialize(
            PlantData plantData,
            PlantSelectionManager selectionManager)
        {
            _plantData = plantData;
            _selectionManager = selectionManager;

            RefreshView();
            RefreshSelectionState(false);
        }

        private void Awake()
        {
            RefreshView();

            if (_button != null)
            {
                _button.onClick.AddListener(HandleClick);
            }
        }

        private void OnDestroy()
        {
            if (_button != null)
            {
                _button.onClick.RemoveListener(HandleClick);
            }
        }

        private void HandleClick()
        {
            if (_plantData == null || _selectionManager == null)
            {
                return;
            }

            _selectionManager.TogglePlant(_plantData);
        }

        private void RefreshView()
        {
            if (_plantData == null)
            {
                return;
            }

            if (_iconImage != null)
            {
                _iconImage.sprite = _plantData.Icon;
            }

            if (_nameText != null)
            {
                _nameText.text = _plantData.PlantName;
            }

            if (_costText != null)
            {
                _costText.text = _plantData.Cost.ToString();
            }
        }

        public void RefreshSelectionState(bool isSelected)
        {
            if (_backgroundImage != null)
            {
                _backgroundImage.color =
                    isSelected ? _selectedColor : _normalColor;
            }
        }
    }
}
