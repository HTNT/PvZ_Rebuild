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
        [SerializeField] private Image[] _selectedSlotIcons;
        [SerializeField] private SelectedPlantSlotUI[] _selectedSlots;

        [Header("Gameplay Cards")]
        [SerializeField] private Transform _gameplayCardPanel;

        [Header("Buttons")]
        [SerializeField] private Button _resetButton;
        [SerializeField] private Button _playButton;

        [Header("Managers")]
        [SerializeField] private GameManager _gameManager;

        private readonly List<PlantData> _selectedPlants = new();
        private readonly List<PlantSelectionCardUI> _selectionCards = new();
        private PlantCardUI[] _gameplayCards;

        private void Awake()
        {
            FindGameplayCards();
            CreateSelectionCards();

            if (_resetButton != null)
            {
                _resetButton.onClick.AddListener(ResetSelection);
            }

            if (_playButton != null)
            {
                _playButton.onClick.AddListener(ConfirmSelection);
            }

            SetGameplayCardsActive(false);
            RefreshUI();
        }

        private void FindGameplayCards()
        {
            if (_gameplayCardPanel == null)
            {
                return;
            }

            _gameplayCards =
                _gameplayCardPanel.GetComponentsInChildren<PlantCardUI>(true);
        }

        private void CreateSelectionCards()
        {
            if (_plantPool == null
                || _selectionCardPrefab == null
                || _gameplayCards == null)
            {
                return;
            }

            foreach (PlantData plantData in _availablePlants)
            {
                PlantSelectionCardUI selectionCard = Instantiate(
                    _selectionCardPrefab,
                    _plantPool
                );

                selectionCard.Initialize(plantData, this);
                _selectionCards.Add(selectionCard);
            }
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
            if (plantData == null)
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

            RefreshUI();
        }

        public void ResetSelection()
        {
            _selectedPlants.Clear();
            RefreshUI();
        }

        private void ConfirmSelection()
        {
            // if (_selectedPlants.Count != _maxSelectedPlants)
            // {
            //     return;
            // }

            SetGameplayCardsActive(true);
            gameObject.SetActive(false);

            if (_gameManager != null)
            {
                _gameManager.StartGame();
            }
        }

        private void SetGameplayCardsActive(bool isGameStarted)
        {
            if (_gameplayCards == null)
            {
                return;
            }

            foreach (PlantCardUI card in _gameplayCards)
            {
                if (card == null)
                {
                    continue;
                }

                bool isSelected = _selectedPlants.Contains(card.PlantData);
                card.gameObject.SetActive(isGameStarted && isSelected);
            }
        }

        private void RefreshUI()
        {
            for (int i = 0; i < _selectedSlots.Length; i++)
            {
                if (i < _selectedPlants.Count)
                {
                    _selectedSlots[i].ShowPlant(_selectedPlants[i]);
                }
                else
                {
                    _selectedSlots[i].Clear();
                }
            }

            foreach (PlantSelectionCardUI card in _selectionCards)
            {
                if (card != null)
                {
                    card.RefreshSelectionState(
                        _selectedPlants.Contains(card.PlantData)
                    );
                }
            }

            if (_playButton != null)
            {
                _playButton.interactable = _selectedPlants.Count > 0;
            }
        }
    }
}
