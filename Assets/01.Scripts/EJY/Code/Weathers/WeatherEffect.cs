using UnityEngine;

namespace Code.Weathers
{
    public abstract class WeatherEffect : MonoBehaviour
    {
        [field: SerializeField] public WeatherType WeatherType { get; private set; }

        public abstract void PlayEffect();
        public abstract void StopEffect();
    }
}