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
            _currentTime = limitTime;
        }

        private void Update()
        {
            if (_currentTime <= 0) return;
            
            _currentTime -= Time.deltaTime;
            float ratio = _currentTime / limitTime;
            barImage.transform.localScale = new Vector3(ratio, 1, 1);
                
            if (_currentTime <= 0)
            {
                _currentTime = 0;
                OnFailEvent?.Invoke(false);
            }
        }
    }
}