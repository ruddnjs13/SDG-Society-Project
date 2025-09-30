using Ami.BroAudio;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI
{
    public class BroAudioSoundSetting : MonoBehaviour
    {
        [SerializeField] private string volumeName;
        [SerializeField] private BroAudioType type;
        
        private Slider _bar;
        private float _volume;

        private void Awake()
        {
            _bar = GetComponentInChildren<Slider>();

            if (PlayerPrefs.HasKey(volumeName))
            {
                _volume = PlayerPrefs.GetFloat(volumeName);
                _bar.value = _volume;
            }
        }

        private void Start()
        {
            BroAudio.SetVolume(type, _volume);
        }

        private void OnDestroy()
        {
            PlayerPrefs.SetFloat(volumeName, _volume);
        }

        public void OnValueChange()
        {
            _volume = _bar.value;
            BroAudio.SetVolume(type, _volume);

        }
    }
}