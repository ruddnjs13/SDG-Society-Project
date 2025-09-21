using Code.Weathers;
using UnityEngine;

namespace LKW.Generaters
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