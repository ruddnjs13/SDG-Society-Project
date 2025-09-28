using DG.Tweening;
using UnityEngine;

namespace Code.Weathers
{
    public class RainyEffect : WeatherEffect
    {
        [SerializeField] private ParticleSystem rainyEffect;

        public override void PlayEffect()
        {
            Sequence seq = DOTween.Sequence();
            seq.Append(DOTween.To(() => _bloom.intensity.value, x => _bloom.intensity.value = x, bloomValue,
                transitionDuration));
            seq.Append(DOTween.To(() => _vignette.intensity.value, x => _vignette.intensity.value = x, vignetteValue,
                transitionDuration));
            

            rainyEffect.Play();
        }

        public override void StopEffect()
        {
            rainyEffect.Stop();
        }
    }
}