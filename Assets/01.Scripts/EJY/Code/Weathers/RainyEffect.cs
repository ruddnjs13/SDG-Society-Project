using UnityEngine;

namespace Code.Weathers
{
    public class RainyEffect : WeatherEffect
    {
        [SerializeField] private ParticleSystem rainyEffect;
        
        public override void PlayEffect()
        {
            rainyEffect.Play();
        }

        public override void StopEffect()
        {
            rainyEffect.Stop();
        }
    }
}