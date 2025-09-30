using System;
using CoinSystem;
using Core.GameEvent;
using DG.Tweening;
using Events;
using InputSystem;
using LKW;
using RuddnjsLib.Dependencies;
using UnityEngine;
using UnityEngine.Events;

namespace LandSystem
{
    public class GeneratorBreaker : MonoBehaviour
    {
        public UnityEvent OnBreakEvent;
        
        [SerializeField] private InputControllerSO inputData;
        [SerializeField] private GameEventChannelSO landChannel;
        [SerializeField] private Transform selectorTrm;
        [Inject] private LandGridManager _landGridM;
        [Inject] private CoinManager _coinM;

        private bool _isActive = false;

        private void Awake()
        {
            SetSelectorActive(false);
        }

        private void Update()
        {
            if (!_isActive) return;
            
            Vector2Int pos = Vector2Int.RoundToInt(inputData.GetWorldPointPos());

            selectorTrm.DOMove((Vector2)pos, 0.2f).SetEase(Ease.OutQuint);
        }

        public void HandleBreakGeneratorStart()
        {
            _isActive = true;
            SetSelectorActive(true);
            inputData.OnSelectPressed += HandleSelectGenerator;
            
            selectorTrm.position = inputData.GetWorldPointPos();
        }

        private void HandleSelectGenerator()
        {
            inputData.OnSelectPressed -= HandleSelectGenerator;
            _isActive = false;
            SetSelectorActive(false);

            if (_coinM.CurrentCoin < 5) return;
            
            Vector2Int pos = Vector2Int.RoundToInt(inputData.GetWorldPointPos());

            if (_landGridM.BreakGenerator(pos))
            {
                OnBreakEvent?.Invoke();
            }
        }

        private void SetSelectorActive(bool isActive)
        {
            selectorTrm.gameObject.SetActive(isActive);
        }
    }
}