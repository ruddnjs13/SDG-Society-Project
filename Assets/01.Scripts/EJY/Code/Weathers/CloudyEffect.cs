using DG.Tweening;

namespace Code.Weathers
{
    public class CloudyEffect : WeatherEffect
    {
        public override void PlayEffect()
        {
            Sequence seq = DOTween.Sequence();
            seq.Append(DOTween.To(() => _bloom.intensity.value, x => _bloom.intensity.value = x, bloomValue,
                transitionDuration));
            seq.Append(DOTween.To(() => _vignette.intensity.value, x => _vignette.intensity.value = x, vignetteValue,
                transitionDuration));
        }

        public override void StopEffect()
        {
            
        }
    }
}