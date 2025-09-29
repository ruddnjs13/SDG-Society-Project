using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Code.UI
{
    public class TimerUI : MonoBehaviour
    {
        [SerializeField] private float limitTime = 1200;
        [SerializeField] private Image barImage;
        private  float _currentTime;

        public UnityEvent<bool> OnFailEvent;

        private void Awake()
        {
            throw new NotImplementedException();
        }

        private void Update()
        {
            _currentTime += Time.deltaTime;
            float ratio = _currentTime / limitTime;

            if (_currentTime >= limitTime)
            {
                OnFailEvent?.Invoke(false);
            }
        }
    }
}