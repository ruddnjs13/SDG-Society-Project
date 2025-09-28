using UnityEngine;

namespace Feedback
{
    public class PlayEffectFeedback : Feedback
    {
        [SerializeField] ParticleSystem particle;
        
        public override void CreateFeedback()
        {
            particle.Play();
        }
    }
}