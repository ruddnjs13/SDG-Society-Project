using Ami.BroAudio;
using UnityEngine;

namespace LKW.Feedbacks
{
    public class SoundFeedback : MonoBehaviour
    {
        [SerializeField] private SoundID soundID;

        public void CreateFeedback()
        {
            BroAudio.Play(soundID.ID);
        }
    }
}