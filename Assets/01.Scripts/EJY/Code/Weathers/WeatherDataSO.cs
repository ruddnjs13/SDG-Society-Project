using System;
using UnityEngine;

namespace Code.Weathers
{
    [CreateAssetMenu(fileName = "WeatherData", menuName = "SO/Weather/Data", order = 0)]
    public class WeatherDataSO : ScriptableObject
    {
        public string weatherName;
        public int nameHash;
        public Sprite weatherIcon;

        private void OnValidate()
        {
            if(string.IsNullOrEmpty(weatherName)) return;
            nameHash = Animator.StringToHash(weatherName);
        }
    }
}