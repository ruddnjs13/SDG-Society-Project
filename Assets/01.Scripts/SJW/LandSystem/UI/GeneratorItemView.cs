using System;
using Core.GameEvent;
using Events;
using LKW.Generators;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LandSystem.UI
{
    public class GeneratorItemView : MonoBehaviour
    {
        [SerializeField] private GameEventChannelSO pointChannel;
        
        [Header("Viewer")]
        [SerializeField] private Image iconImage;
        [SerializeField] private TextMeshProUGUI generatorNameText;
        [SerializeField] private TextMeshProUGUI generatorValueText;
        [SerializeField] private TextMeshProUGUI generatorPenaltyText;
        [SerializeField] private TextMeshProUGUI buyButtonText;
        [SerializeField] private Button buyButton;

        private GeneratorDataSO currentData;
        
        public void Initialize(GeneratorDataSO data)
        {
            iconImage.sprite = data.generatorVisual;
            generatorNameText.SetText(data.generatorName);
            generatorValueText.SetText($"+{Math.Round((data.generateAmount * data.amountMultiplier) / data.generateTime, 2)} E/s");
            generatorPenaltyText.SetText(""); //임시
            buyButtonText.SetText($"구매: {data.needCoinCount} Coin");
            
            buyButton.onClick.AddListener(HandleRequestBuy);
            currentData = data;
        }

        private void HandleRequestBuy()
        {
            var evt = PointEvents.RequestGeneratorBuyEvent.Initializer(currentData);
            pointChannel.RaiseEvent(evt);
        }
    }
}