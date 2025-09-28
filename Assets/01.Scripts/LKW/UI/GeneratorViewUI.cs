using System;
using System.Net.NetworkInformation;
using LKW.Generators;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LKW.UI
{
    public class GeneratorViewUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI headerText;
        [SerializeField] private TextMeshProUGUI amountText;
        [SerializeField] private TextMeshProUGUI durationText;

        [SerializeField] private Transform goodParent;
        [SerializeField] private Transform badParent;

        [SerializeField] private GameObject weatherIconItem;
        [SerializeField] private Sprite goodIcon;
        [SerializeField] private Sprite badIcon;


        public void SetView(GeneratorDataSO generatorData, float amountMultiplier)
        {
            Debug.Log(generatorData);
            headerText.text = generatorData.generatorName;
            amountText.text = $"발전량 : {amountMultiplier * generatorData.generateAmount}";
            durationText.text =  $"발전시간 : {generatorData.generateTime} 초";

            foreach (Transform child in goodParent)
            {
                Destroy(child.gameObject);
            }
            
            foreach (Transform child in badParent)
            {
                Destroy(child.gameObject);
            }
            
            Instantiate(weatherIconItem, goodParent).GetComponent<Image>().sprite =goodIcon;
            Instantiate(weatherIconItem, badParent).GetComponent<Image>().sprite = badIcon;

            for (int i = 0; i < generatorData.goodWeatherVisual.Length; i++)
            {
                Instantiate(weatherIconItem, goodParent).GetComponent<Image>().sprite = generatorData.goodWeatherVisual[i];
            }
            for (int i = 0; i < generatorData.badWeatherVisual.Length; i++)
            {
                Instantiate(weatherIconItem, badParent).GetComponent<Image>().sprite = generatorData.badWeatherVisual[i];
            }
        }
    }
}