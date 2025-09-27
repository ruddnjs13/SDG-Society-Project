using Core.GameEvent;
using LKW.Generators;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LandSystem.UI
{
    public class GeneratorItemView : MonoBehaviour
    {
        [SerializeField] private Image iconImage;
        [SerializeField] private GameEventChannelSO pointChannel;
        [SerializeField] private TextMeshProUGUI generatorNameText;
        [SerializeField] private TextMeshProUGUI generatorValueText;
        [SerializeField] private Button buyButton;
        
        public void Initialize(GeneratorDataSO data)
        {
            buyButton.onClick.AddListener(HandleRequestBuy);
        }

        private void HandleRequestBuy()
        {
        }
    }
}