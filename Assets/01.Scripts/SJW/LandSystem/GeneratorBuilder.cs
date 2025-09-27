using _01.Scripts.SJW.Events;
using InputSystem;
using Core.GameEvent;
using DG.Tweening;
using Events;
using LKW.Generators;
using UnityEngine;

namespace LandSystem
{
    public class GeneratorBuilder : MonoBehaviour
    {
        [SerializeField] private GameEventChannelSO pointChannel; //임시
        [SerializeField] private GameEventChannelSO landChannel; 
        [SerializeField] private InputControllerSO inputData;
        
        [SerializeField] private SpriteRenderer possibleChecker;
        [SerializeField] private SpriteRenderer buildingIcon;

        private GeneratorDataSO _currentData;

        private bool _isEnable;
        
        private void Awake()
        {
            pointChannel.AddListener<RequestGeneratorBuyEvent>(HandleGeneratorBuildStart);
            SetEnable(false);
        }

        private void OnDestroy()
        {
            pointChannel.RemoveListener<RequestGeneratorBuyEvent>(HandleGeneratorBuildStart);
        }

        private void Update()
        {
            if (!_isEnable) return;
            var pos = Vector2Int.RoundToInt(inputData.GetWorldPointPos());
            
            buildingIcon.transform.DOMove((Vector2)pos, 0.2f);
            
        }

        private void HandleGeneratorBuildStart(RequestGeneratorBuyEvent evt)
        {
            inputData.OnSelectPressed += HandleBuildGenerator;

            SetEnable(true);
            
            _currentData = evt.generatorData;
            buildingIcon.sprite = evt.generatorData.generatorVisual;
        }

        private void HandleBuildGenerator()
        {
            SetEnable(false);
            inputData.OnSelectPressed -= HandleBuildGenerator;

            var evt = LandEvents.BuildRequestEvent.Initializer(_currentData, inputData.GetWorldPointPos());
            landChannel.RaiseEvent(evt);
        }
        
        private void SetEnable(bool isEnable)
        {
            _isEnable = isEnable;
            buildingIcon.gameObject.SetActive(isEnable);
            possibleChecker.gameObject.SetActive(isEnable);
        }
    }
}