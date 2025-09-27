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
        [SerializeField] private Button buyButton;

        private int currentAmount;
        
        public void Initialize(GeneratorDataSO data)
        {
            iconImage.sprite = data.generatorVisual;
            generatorNameText.SetText(data.generatorName);
            generatorValueText.SetText($"+{(data.generateAmount * data.amountMultiplier) / data.generateTime}E/s");
            generatorPenaltyText.SetText(""); //임시
            
            buyButton.onClick.AddListener(HandleRequestBuy);
            currentAmount = (int)data.generateAmount;
        }

        private void HandleRequestBuy()
        {
            var evt = PointEvents.RequestGeneratorBuyEvent.Initializer(currentAmount);
            pointChannel.RaiseEvent(evt);
        }
    }
}