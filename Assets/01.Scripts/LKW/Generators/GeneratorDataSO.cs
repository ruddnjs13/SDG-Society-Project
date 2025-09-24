using Code.Weathers;
using UnityEngine;
using UnityEngine.Serialization;

namespace LKW.Generators
{
    [CreateAssetMenu(fileName = "GeneratorDataSO", menuName = "SO/GeneratorDataSO", order = 0)]
    public class GeneratorDataSo : ScriptableObject
    {
        public Sprite generatorVisual;
        public float generateTime;
        public float generateAmount;
        public float amountMultiplier;
        public WeatherType weatherType;
    }
}