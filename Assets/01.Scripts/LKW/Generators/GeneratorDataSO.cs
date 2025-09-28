using Code.Weathers;
using LKW.Generaters;
using UnityEngine;
using UnityEngine.Serialization;

namespace LKW.Generators
{
    [CreateAssetMenu(fileName = "GeneratorDataSO", menuName = "SO/GeneratorDataSO", order = 0)]
    public class GeneratorDataSO : ScriptableObject
    {
        [Header("Names")]
        public string generatorName;
        public string generatorKorName;
        
        [Header("Want types")]
        public GeneratorType generatorType;
        public WeatherType goodWeatherType;
        public WeatherType badWeatherType;
        public TimeZoneType timeZoneType;
        
        [Header("Visuals")]
        public Sprite generatorVisual;
        
        [Header("Values")]
        public float generateTime;
        public float generateAmount;
        public float amountMultiplier;
        public int needCoinCount;
        public bool isNeedWater;
    }
}