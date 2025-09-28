using UnityEngine;

namespace Feedback
{
    public abstract class Feedback : MonoBehaviour
    {
        public abstract void CreateFeedback();

        public virtual void StopFeedback()
        {
            
        }
    }
}