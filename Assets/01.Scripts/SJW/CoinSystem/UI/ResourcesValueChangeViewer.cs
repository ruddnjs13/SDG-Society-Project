using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace CoinSystem.UI
{
    public class ResourcesValueChangeViewer : MonoBehaviour
    {
        [SerializeField] private string targetResources;
        [SerializeField] private TextMeshProUGUI currentValueText;
        [SerializeField] private Color highlightColor;
        [SerializeField] private Color rowColor;

        public void HandleChangeValue(int current, int prev, bool isDirect)
        {
            var value = prev;

            if (isDirect)
            {
                currentValueText.SetText(current.ToString());
                return;
            }
            
            currentValueText.DOColor(current > prev ? highlightColor : rowColor, 0.1f);
            currentValueText.transform.DOScale(1.35f, 0.2f).SetEase(Ease.OutCubic);
            DOTween.To(() => value, x =>
            {
                value = x;
                currentValueText.SetText(x.ToString());
            }, current, 0.2f).OnComplete(() =>
            {
                currentValueText.DOColor(Color.white, 0.1f);
                currentValueText.transform.DOScale(1f, 0.2f).SetEase(Ease.InQuart);
            });
        }
    }
}