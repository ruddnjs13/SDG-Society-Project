using System;
using _01.Scripts.LKW.Generators;
using LKW.Generaters.LKW.Events;
using Code.Weathers;
using Code.Weathers.Utility;
using Core.GameEvent;
using LandSystem;
using RuddnjsPool;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using VHierarchy.Libs;

namespace LKW.Generators
{
    public class Generator : MonoBehaviour
    {
        public UnityEvent generatorEvent;
        
        [SerializeField] private PoolManagerSO poolManager;
        [SerializeField] private GameEventChannelSO energyChannel;
        [SerializeField] private PoolingItemSO energyItemPrefab;
        
        private GeneratorRenderer _generatorRenderer;
        
        private  float _generateAmount;
        public  float AmountMultiplier { get; private set; }
        
        public float GenerateTime {get; private set;}
        public float RemainingTime { get; set; } = 0f;
        
        public SendEnvironmentData TimeZoneData {get; set;}
        public SendEnvironmentData GoodWeatherData {get; set;}
        public SendEnvironmentData BadWeatherData {get; set;}
        
        public GeneratorDataSO MyData { get; private set; }

        public bool IsRunning { get; private set; } = false;

        private void Awake()
        {
            _generatorRenderer = GetComponent<GeneratorRenderer>();
        }

        public void Initialize(GeneratorDataSO generatorData)
        {
            MyData = generatorData;
            
            GenerateTime = generatorData.generateTime;
            _generateAmount = generatorData.generateAmount;
            AmountMultiplier = generatorData.amountMultiplier;
            _generatorRenderer.InitVisual(generatorData.generatorVisual);
            _generatorRenderer.SetVisualByWeather(IsRunning, AmountMultiplier);

            GoodWeatherData = new SendEnvironmentData()
            {
                TypeBit = (int)generatorData.goodWeatherType
            };
            
            BadWeatherData = new SendEnvironmentData()
            {
                TypeBit = (int)generatorData.badWeatherType,
            };

            TimeZoneData = new SendEnvironmentData()
            {
                TypeBit = (int)generatorData.timeZoneType
            };
        }

        public void GenerateEnergy()
        {
            generatorEvent?.Invoke();
            
            int getAmount = Mathf.RoundToInt(_generateAmount * AmountMultiplier); 
            
            GetEnergyEvent evt = EnergyEvents.GetEnergyEvent.Initializer(getAmount);
            energyChannel.RaiseEvent(evt);
            RemainingTime = 0f;
            
            GetEnergyView energyView = poolManager.Pop(energyItemPrefab) as GetEnergyView;
            energyView.ShowEnergyView(transform.position+ new Vector3(0,0.5f,0), getAmount);
        }

        public void UpdateEnvironment(SendEnvironmentData currentData)
        {
            if (!currentData.CanWorkByWeather(TimeZoneData))
            {
                AmountMultiplier = 1f;
                StopGenerate();
                return;
            }

            if (currentData.CanWorkByWeather(GoodWeatherData))
                AmountMultiplier = 1.5f;
            else if (currentData.CanWorkByWeather(BadWeatherData))
                AmountMultiplier = 0.5f;
            else
                AmountMultiplier = 1f;
            
            StartGenerate();
        }
        
        
        public void StartGenerate()
        {
            IsRunning = true;
            _generatorRenderer.SetVisualByWeather(IsRunning, AmountMultiplier);
            RemainingTime = 0;
        }

        public void StopGenerate()
        {
            IsRunning = false;
            _generatorRenderer.SetVisualByWeather(IsRunning, AmountMultiplier);
        }

        [ContextMenu("Add Generator")]
        public void AddGenerator()
        {
            GeneratorManager.Instance.AddGenerator(this);
        }
    }
}
