using System;
using DG.Tweening;
using UnityEngine;

namespace Code.UI
{
    public class ButtonEventTriggers : MonoBehaviour
    {
        [SerializeField] private float duration = 0.2f;
        [SerializeField] private float sizeMultiplier = 1.5f;

        private Vector3 _originScale;

        private void Awake()
        {
            _originScale = transform.localScale;
        }

        public void OnQuit() => Application.Quit();
        
        public void MouseOver()
        {
            transform.DOScale(_originScale * sizeMultiplier, duration);
        }

        public void MouseOut()
        {
            transform.DOScale(_originScale, duration);
        }
    }
}