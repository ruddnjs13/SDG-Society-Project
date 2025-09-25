using DG.Tweening;
using UnityEngine;

namespace Code.Weathers
{
    public class NormalEffect : WeatherEffect
    {
        [SerializeField] private float bloomValue = 1f;
        [SerializeField] private float vignetteValue = 0f;
        
        public override void PlayEffect()
        { }

        public override void StopEffect()
        { }

        public override void TransitionBloom()
        {
            Sequence seq = DOTween.Sequence();
            seq.Append(DOTween.To(() => _bloom.intensity.value, x => _bloom.intensity.value = x, bloomValue,transitionDuration));
            seq.Append(DOTween.To(() => _vignette.intensity.value, x => _vignette.intensity.value = x, vignetteValue,transitionDuration));
        }
    }
}