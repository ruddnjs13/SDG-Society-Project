using System;
using DG.Tweening;
using RuddnjsPool;
using UnityEngine;
using UnityEngine.Serialization;

namespace LKW.Generators
{
    public class GetEnergyView : MonoBehaviour, IPoolable
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
         [field:SerializeField] public PoolingItemSO PoolingType { get; set; }
        public GameObject GameObject => gameObject;

        [SerializeField] private float showDuration = 1;
        [SerializeField] private float moveTime = 0.8f;
        [SerializeField] private float scaleTime = 0.3f;
        [SerializeField] private float fadeTime = 0.3f;

        private void OnEnable()
        {
            ShowEnergyView();
        }

        public void ShowEnergyView()
        {
            Sequence seq  = DOTween.Sequence();
            
            seq.Append(transform.DOScale(1.2f, scaleTime));
            seq.AppendInterval(showDuration);
            seq.Join(spriteRenderer.DOFade(1,fadeTime));
            seq.Join(transform.DOMoveY(0.6f, moveTime));
            seq.Append(spriteRenderer.DOFade(0, fadeTime));
        }
        

        private Pool _myPool;
        
        public void SetUpPool(Pool pool)
        {
            _myPool = pool;
        }

        public void ResetItem()
        {
        }
    }
}