using System;
using _01.Scripts.LKW.Generators;
using InputSystem;
using LKW.Generators;
using UnityEngine;

namespace LKW.UI
{
    public class RuntimeUI : MonoBehaviour
    {
        private GeneratorViewUI generatorViewUI;
        [SerializeField] private InputControllerSO inputController;

        private void Awake()
        {
            generatorViewUI = GetComponentInChildren<GeneratorViewUI>();
        }

        private void OnEnable()
        {
            inputController.OnSelectPressed += HandleSelectPressed;
        }

        private void OnDestroy()
        {
            inputController.OnSelectPressed -= HandleSelectPressed;
        }

        private void HandleSelectPressed()
        {
            Debug.Log("select");
            Vector2 origin = inputController.GetWorldPointPos();

            Collider2D hit = Physics2D.OverlapPoint(origin, LayerMask.GetMask("Building"));

            if (hit != null && hit.TryGetComponent(out Generator generator))
            {
                Debug.Log(generator.MyData);
                Debug.Log(generatorViewUI);
                generatorViewUI.SetView(generator.MyData, generator.AmountMultiplier);
            }
        }
    }
}