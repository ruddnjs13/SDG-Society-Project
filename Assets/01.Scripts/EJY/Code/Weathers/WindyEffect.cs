using UnityEngine;

namespace Code.Weathers
{
    public class WindyEffect : WeatherEffect
    {
        [SerializeField] private ParticleSystem windyParticle;
        public override void PlayEffect()
        {
            windyParticle.Play();
        }

        public override void StopEffect()
        {
            windyParticle.Stop();
        }
    }
}