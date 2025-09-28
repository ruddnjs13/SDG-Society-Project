using LKW.Generators;
using TMPro;
using UnityEngine;

namespace LKW.UI
{
    public class GeneratorViewUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI headerText;
        [SerializeField] private TextMeshProUGUI amountText;
        [SerializeField] private TextMeshProUGUI durationText;

        public void SetView(GeneratorDataSO generatorData, int amountMultiplier)
        {
            headerText.SetText(generatorData.generatorKorName);
            amountText.SetText($"발전량 : {amountMultiplier * generatorData.generateAmount}");
            durationText.SetText($"발전시간 : {generatorData.generateTime}");
        }
    }
}