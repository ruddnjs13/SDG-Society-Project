using UnityEngine;

namespace Code.Weathers
{
    [CreateAssetMenu(fileName = "WeatherData", menuName = "SO/Weather/Data", order = 0)]
    public class WeatherDataSO : ScriptableObject
    {
        public WeatherType weatherType;
        public Sprite weatherIcon;
    }
}