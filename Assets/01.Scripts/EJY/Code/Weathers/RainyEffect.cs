using DG.Tweening;
using UnityEngine;

namespace Code.Weathers
{
    public class RainyEffect : WeatherEffect
    {
        [SerializeField] private ParticleSystem rainyEffect;

        public override void PlayEffect()
        {
            float bloomVal = _bloom.intensity.value;
            float vignetteVal = _vignette.intensity.value;

            Sequence seq = DOTween.Sequence();
            seq.Append(
                DOTween.To(() => bloomVal, x => {
                    bloomVal = x;
                    _bloom.intensity.value = bloomVal;
                }, bloomValue, transitionDuration)
            );
            seq.Join(
                DOTween.To(() => vignetteVal, x => {
                    vignetteVal = x;
                    _vignette.intensity.value = vignetteVal;
                }, vignetteValue, transitionDuration)
            );
            
            rainyEffect.Play();
        }

        public override void StopEffect()
        {
            rainyEffect.Stop();
        }
    }
}