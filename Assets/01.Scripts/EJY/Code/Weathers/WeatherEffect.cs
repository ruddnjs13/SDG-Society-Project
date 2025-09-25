using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Code.Weathers
{
    public abstract class WeatherEffect : MonoBehaviour
    {
        [field: SerializeField] public WeatherType WeatherType { get; private set; }
        [SerializeField] protected float transitionDuration = 0.2f;
        protected Bloom _bloom;
        protected Vignette _vignette;

        public abstract void PlayEffect();
        public abstract void StopEffect();
        public abstract void TransitionBloom();

        public virtual void Init(Bloom bloom, Vignette vignette)
        {
            _bloom = bloom;
            _vignette = vignette;
        }
    }
}