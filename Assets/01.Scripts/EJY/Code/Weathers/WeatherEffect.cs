using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Code.Weathers
{
    public abstract class WeatherEffect : MonoBehaviour
    {
        [field: SerializeField] public WeatherType WeatherType { get; private set; }
        [SerializeField] protected float transitionDuration = 0.2f;
        [SerializeField] protected float bloomValue = 1f;
        [SerializeField] protected float vignetteValue = 0f;
        protected Bloom _bloom;
        protected Vignette _vignette;

        public abstract void PlayEffect();
        public abstract void StopEffect();
        public virtual void Init(Volume volume)
        {
            if (volume.profile.TryGet(out Bloom bloom))
            {
                _bloom = bloom;
            }

            if (volume.profile.TryGet(out Vignette vignette))
            {
                _vignette = vignette;
            }
        }
    }
}