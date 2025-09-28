using _01.Scripts.SJW.Events;
using InputSystem;
using Core.GameEvent;
using DG.Tweening;
using Events;
using LKW.Generators;
using RuddnjsLib.Dependencies;
using UnityEngine;

namespace LandSystem
{
    public class GeneratorBuilder : MonoBehaviour
    {
        [SerializeField] private GameEventChannelSO landChannel; 
        [SerializeField] private InputControllerSO inputData;
        
        [SerializeField] private SpriteRenderer possibleChecker;
        [SerializeField] private SpriteRenderer buildingIcon;

        [Header("Colors")]
        [SerializeField] private Color worryColor;
        [SerializeField] private Color possibleColor;
        
        [Inject] private LandGridManager landGridM;
        private GeneratorDataSO _currentData;

        private bool _isEnable;
        private bool _prevCanBuild;
        
        private void Awake()
        {
            landChannel.AddListener<BuyCompleteGeneratorEvent>(HandleGeneratorBuildStart);
            SetEnable(false);
        }

        private void OnDestroy()
        {
            landChannel.RemoveListener<BuyCompleteGeneratorEvent>(HandleGeneratorBuildStart);
        }

        private void Update()
        {
            if (!_isEnable) return;
            var pos = Vector2Int.RoundToInt(inputData.GetWorldPointPos());
            
            buildingIcon.transform.DOMove((Vector2)pos, 0.2f).SetEase(Ease.OutSine);

            if (_prevCanBuild == landGridM.IsPossibleBuild(pos, _currentData.isNeedWater)) return;
            _prevCanBuild = landGridM.IsPossibleBuild(pos, _currentData.isNeedWater);

            possibleChecker.color = _prevCanBuild ? possibleColor : worryColor;
        }

        private void HandleGeneratorBuildStart(BuyCompleteGeneratorEvent evt)
        {
            inputData.OnSelectPressed += HandleBuildGenerator;
            inputData.OnCancelPressed += HandleCancelPressed;

            SetEnable(true);
            
            _currentData = evt.generatorData;
            buildingIcon.sprite = evt.generatorData.generatorVisual;
            buildingIcon.transform.position = inputData.GetWorldPointPos();
        }

        private void HandleCancelPressed()
        {
            Cancel();
            _currentData = null;
        }

        private void HandleBuildGenerator()
        {
            Cancel();

            if (_currentData == null) return;
            
            var evt = LandEvents.BuildRequestEvent.Initializer(_currentData, inputData.GetWorldPointPos());
            landChannel.RaiseEvent(evt);
        }
        
        private void Cancel()
        {
            inputData.OnSelectPressed -= HandleBuildGenerator;
            inputData.OnCancelPressed -= HandleCancelPressed;
            
            SetEnable(false);
        }
        
        private void SetEnable(bool isEnable)
        {
            _isEnable = isEnable;
            buildingIcon.gameObject.SetActive(isEnable);
            possibleChecker.gameObject.SetActive(isEnable);
        }
    }
}