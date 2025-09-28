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

        public void HandleChangeValue(int current, int prev)
        {
            var value = prev;
            DOTween.To (() =>value, x =>
            {
                value = x;
                currentValueText.SetText(x.ToString());
            }, current, 0.1f);
        }
    }
}