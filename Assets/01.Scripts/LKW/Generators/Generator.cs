using System;
using LKW.Generaters.LKW.Events;
using Code.Weathers;
using Core.GameEvent;
using RuddnjsPool;
using UnityEngine;
using UnityEngine.Serialization;

namespace LKW.Generators
{
    public class Generator : MonoBehaviour
    {
        [SerializeField] private GeneratorDataSo generatorData;
        [SerializeField] private PoolManagerSO poolManager;
        [SerializeField] private GameEventChannelSO energyChannel;
        [SerializeField] private PoolingItemSO energyItemPrefab;
        [SerializeField] private SpriteRenderer spriteRenderer;
        
        private  float _generateAmount;
        private  float _amountMultiplier;
        public  WeatherType WeatherType { get; private set; }
        public float GenerateTime {get; private set;}
        public float RemainingTime { get; set; } = 0f;

        private void Start()
        {
            Initialize(generatorData);
        }

        public void Initialize(GeneratorDataSo generatorData)
        {
            GenerateTime = generatorData.generateTime;
            WeatherType = generatorData.weatherType;
            _generateAmount = generatorData.generateAmount;
            _amountMultiplier = generatorData.amountMultiplier;

            spriteRenderer.sprite = generatorData.generatorVisual;
        }

        public void GenerateEnergy()
        {
            int getAmount = Mathf.RoundToInt(_generateAmount * _amountMultiplier); 
            
            GetEnergyEvent evt = EnergyEvents.GetEnergyEvent.Initializer(getAmount);
            energyChannel.RaiseEvent(evt);
            RemainingTime = 0f;
            
            GetEnergyView energyView = poolManager.Pop(energyItemPrefab) as GetEnergyView;
            energyView.ShowEnergyView(transform.position+ new Vector3(0,0.5f,0));
        }

        [ContextMenu("Add Generator")]
        public void AddGenerator()
        {
            GeneratorManager.Instance.AddGenerator(this);
        }
    }
}
