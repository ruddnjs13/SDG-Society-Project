using Code.Weathers;
using LKW.Generaters;
using UnityEngine;
using UnityEngine.Serialization;

namespace LKW.Generators
{
    [CreateAssetMenu(fileName = "GeneratorDataSO", menuName = "SO/GeneratorDataSO", order = 0)]
    public class GeneratorDataSO : ScriptableObject
    {
        public string generatorName;
        public GeneratorType generatorType;
        public WeatherType goodWeatherType;
        public WeatherType badWeatherType;
        public TimeZoneType timeZoneType;
        public Sprite generatorVisual;
        public float generateTime;
        public float generateAmount;
        public float amountMultiplier;
        public bool isNeedWater;
    }
}