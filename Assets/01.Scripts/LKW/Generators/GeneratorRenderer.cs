using System;
using UnityEngine;

namespace _01.Scripts.LKW.Generators
{
    public class GeneratorRenderer : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        [SerializeField] private GameObject buffVisual;
        [SerializeField] private GameObject debuffVisual;
        
        private readonly Color _stopRunningColor = new Color(0.6f, 0.6f, 0.6f);
        private readonly Color _runningColor = Vector4.one;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void InitVisual(Sprite visual)
        {
            _spriteRenderer.sprite = visual;
        }


        public void SetVisualByWeather(bool isRunning, float amountMultiplier = 1f)
        {
            _spriteRenderer.color = isRunning ? _runningColor : _stopRunningColor;

            if (Mathf.Approximately(amountMultiplier, 1f))
            {
                buffVisual.SetActive(false);
                debuffVisual.SetActive(false);
            }
            else if (amountMultiplier < 1f)
            {
                buffVisual.SetActive(false);
                debuffVisual.SetActive(true);
            }
            else if (amountMultiplier > 1f)
            {
                buffVisual.SetActive(true);
                debuffVisual.SetActive(false);
            }
        }
        
        
    }
}