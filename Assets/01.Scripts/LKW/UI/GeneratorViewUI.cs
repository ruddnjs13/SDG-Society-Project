using System;
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

        private void Start()
        {
        }

        public void SetView(GeneratorDataSO generatorData, float amountMultiplier)
        {
            Debug.Log(generatorData);
            headerText.text = generatorData.generatorKorName;
            amountText.text = $"발전량 : {amountMultiplier * generatorData.generateAmount}";
            durationText.text =  $"발전시간 : {generatorData.generateTime}";
        }
    }
}