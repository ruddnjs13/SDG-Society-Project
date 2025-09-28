using System;
using DG.Tweening;
using RuddnjsPool;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using IPoolable = RuddnjsPool.IPoolable;
using Sequence = DG.Tweening.Sequence;

namespace LKW.Generators
{
    public class GetEnergyView : MonoBehaviour, IPoolable
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private TextMeshPro amountText;
        [field:SerializeField] public PoolingItemSO PoolingType { get; set; }
        public GameObject GameObject => gameObject;

        [SerializeField] private float showDuration = 1;
        [SerializeField] private float moveTime = 1f;
        [SerializeField] private float scaleTime = 0.6f;
        [SerializeField] private float fadeTime = 0.6f;

        public void ShowEnergyView(Vector3 position, int getAmount)
        {
            transform.position = position;

            amountText.SetText($"+{getAmount}");

            Sequence seq = DOTween.Sequence();

            seq.Append(transform.DOScale(1.2f, scaleTime));

            seq.Join(spriteRenderer.DOFade(1, fadeTime));
            seq.Join(amountText.DOFade(1, fadeTime));
            seq.Join(transform.DOMoveY(transform.position.y + 0.6f, moveTime));

            seq.AppendInterval(showDuration);

            seq.Append(spriteRenderer.DOFade(0, fadeTime));
            seq.Join(amountText.DOFade(0, fadeTime));

            seq.AppendCallback(() =>
            {
                _myPool.Push(this);
            });
        }

        private Pool _myPool;
        
        public void SetUpPool(Pool pool)
        {
            _myPool = pool;
        }

        public void ResetItem()
        {
            transform.DOKill();
            spriteRenderer.DOKill();

            transform.position = Vector3.zero;
            transform.localScale = Vector3.one;
            var color = spriteRenderer.color;
            color.a = 1f;
            spriteRenderer.color = color;
        }
    }
}