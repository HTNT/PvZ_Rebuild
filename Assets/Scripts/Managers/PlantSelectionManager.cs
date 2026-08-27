using System.Collections.Generic;
using PVZ_MVS.Scripts.Data;
using PVZ_MVS.Scripts.UI;
using UnityEngine;
using UnityEngine.UI;

namespace PVZ_MVS.Scripts.Managers
{
    public class PlantSelectionManager : MonoBehaviour
    {
        [Header("Selection")]
        [SerializeField] private PlantData[] _availablePlants;
        [SerializeField, Min(1)] private int _maxSelectedPlants = 9;
        [SerializeField] private Transform _plantPool;
        [SerializeField] private PlantSelectionCardUI _selectionCardPrefab;

        [Header("Selected Bar")]
        [SerializeField] private SelectedPlantSlotUI[] _selectedSlots;
        [SerializeField] private PlantPlacement _plantPlacement;

        [Header("Buttons")]
        [SerializeField] private Button _resetButton;
        [SerializeField] private Button _playButton;

        [Header("Managers")]
        [SerializeField] private GameManager _gameManager;

        private readonly List<PlantData> _selectedPlants = new();
        private readonly List<PlantSelectionCardUI> _selectionCards = new();
        private bool _isSelectionLocked;

        private void Awake()
        {
            CreateSelectionCards();

            if (_resetButton != null)
            {
                _resetButton.onClick.AddListener(ResetSelection);
            }

            if (_playButton != null)
            {
                _playButton.onClick.AddListener(ConfirmSelection);
            }

            RefreshPreviewUI();
        }

        private void OnDestroy()
        {
            if (_resetButton != null)
            {
                _resetButton.onClick.RemoveListener(ResetSelection);
            }

            if (_playButton != null)
            {
                _playButton.onClick.RemoveListener(ConfirmSelection);
            }
        }

        public void TogglePlant(PlantData plantData)
        {
            if (_isSelectionLocked || plantData == null)
            {
                return;
            }

            if (_selectedPlants.Contains(plantData))
            {
                _selectedPlants.Remove(plantData);
            }
            else
            {
                if (_selectedPlants.Count >= _maxSelectedPlants)
                {
                    return;
                }

                _selectedPlants.Add(plantData);
            }

            RefreshPreviewUI();
        }

        public void ResetSelection()
        {
            if (_isSelectionLocked)
            {
                return;
            }

            _selectedPlants.Clear();
            RefreshPreviewUI();
        }

        private void ConfirmSelection()
        {
            if (_selectedPlants.Count == 0 || _isSelectionLocked)
            {
                return;
            }

            _isSelectionLocked = true;
            LockSelectedBar();
            gameObject.SetActive(false);

            if (_gameManager != null)
            {
                _gameManager.StartGame();
            }
        }

        private void CreateSelectionCards()
        {
            if (_plantPool == null || _selectionCardPrefab == null)
            {
                return;
            }

            foreach (PlantData plantData in _availablePlants)
            {
                if (plantData == null)
                {
                    continue;
                }

                PlantSelectionCardUI selectionCard = Instantiate(
                    _selectionCardPrefab,
                    _plantPool
                );

                selectionCard.Initialize(plantData, this);
                _selectionCards.Add(selectionCard);
            }
        }

        private void RefreshPreviewUI()
        {
            for (int i = 0; i < _selectedSlots.Length; i++)
            {
                if (i < _selectedPlants.Count)
                {
                    _selectedSlots[i].ShowPreview(_selectedPlants[i]);
                }
                else
                {
                    _selectedSlots[i].Clear();
                }
            }

            foreach (PlantSelectionCardUI card in _selectionCards)
            {
                card.RefreshSelectionState(
                    _selectedPlants.Contains(card.PlantData)
                );
            }

            if (_playButton != null)
            {
                _playButton.interactable = _selectedPlants.Count > 0;
            }
        }

        private void LockSelectedBar()
        {
            for (int i = 0; i < _selectedSlots.Length; i++)
            {
                if (i < _selectedPlants.Count)
                {
                    _selectedSlots[i].LockForGameplay(
                        _selectedPlants[i],
                        _plantPlacement
                    );
                }
                else
                {
                    _selectedSlots[i].Clear();
                }
            }
        }
    }
}
