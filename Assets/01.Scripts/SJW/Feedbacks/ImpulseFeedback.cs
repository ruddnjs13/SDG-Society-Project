using Unity.Cinemachine;
using UnityEngine;

namespace Feedback
{
    [RequireComponent(typeof(CinemachineImpulseSource))]
    public class ImpulseFeedback : Feedback
    {
        [SerializeField] private CinemachineImpulseSource source;
        [SerializeField] private float impulsePower;
        
        public override void CreateFeedback()
        {
            source.GenerateImpulse(impulsePower);
        }
    }
}