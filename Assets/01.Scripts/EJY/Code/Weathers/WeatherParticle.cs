using UnityEngine;

namespace Code.Weathers
{
    public class WeatherParticle : MonoBehaviour
    {
        [field: SerializeField] public WeatherType WeatherType { get; private set; }
        [SerializeField] private ParticleSystem weatherParticle;
        
        public void PlayParticle() => weatherParticle.Play();
        public void StopParticle() => weatherParticle.Stop();
    }
}