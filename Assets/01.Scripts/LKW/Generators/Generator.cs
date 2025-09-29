using System;
using _01.Scripts.LKW.Generators;
using LKW.Generaters.LKW.Events;
using Code.Weathers;
using Code.Weathers.Utility;
using Core.GameEvent;
using RuddnjsPool;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace LKW.Generators
{
    public class Generator : MonoBehaviour
    {
        public UnityEvent OnBuildEvent;
        [FormerlySerializedAs("generatorEvent")] public UnityEvent penaltyEvent;
        
        [SerializeField] private PoolManagerSO poolManager;
        [SerializeField] private GameEventChannelSO energyChannel;
        [SerializeField] private PoolingItemSO energyItemPrefab;
        
        private GeneratorRenderer _generatorRenderer;
        
        [FormerlySerializedAs("_generateAmount")] public  float generateAmount;
        public  float AmountMultiplier { get; private set; }
        
        public float GenerateTime {get; private set;}
        public float RemainingTime { get; set; } = 0f;
        
        public SendEnvironmentData TimeZoneData {get; set;}
        public SendEnvironmentData GoodWeatherData {get; set;}
        public SendEnvironmentData BadWeatherData {get; set;}
        
        public GeneratorDataSO MyData { get; private set; }

        public bool IsRunning { get; private set; } = false;
        public bool IsPenalty { get; private set; } = false;

        private void Awake()
        {
            _generatorRenderer = GetComponent<GeneratorRenderer>();
        }

        public void Initialize(GeneratorDataSO generatorData)
        {
            MyData = generatorData;
            
            IsPenalty = generatorData.isPenalty;
            GenerateTime = generatorData.generateTime;
            generateAmount = generatorData.generateAmount;
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
            
            OnBuildEvent?.Invoke();
        }

        public void GenerateEnergy()
        {
            if(IsPenalty)
                penaltyEvent?.Invoke();
            
            int getAmount = Mathf.CeilToInt(generateAmount * AmountMultiplier); 
            
            Debug.Log(getAmount);
            
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

            AmountMultiplier = 1f;

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
