using Code.Weathers;
using LKW.Generaters;
using UnityEngine;

namespace LKW.Generators
{
    [CreateAssetMenu(fileName = "GeneratorDataSO", menuName = "SO/GeneratorDataSO", order = 0)]
    public class GeneratorDataSO : ScriptableObject
    {
        public WeatherType weatherType;
        public float generateTime;
        public float generateAmount;
        public float amountMultiplier;
    }
}