using InputSystem;
using LKW.Generators;
using UnityEngine;

namespace LKW.UI
{
    public class RuntimeUI : MonoBehaviour
    {
        [SerializeField] private GeneratorViewUI generatorViewUI;
        [SerializeField] private InputControllerSO inputController;

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
            Vector2 origin = inputController.GetWorldPointPos();

            Collider2D hit = Physics2D.OverlapPoint(origin, LayerMask.GetMask("Building"));

            if (hit != null && hit.TryGetComponent(out Generator generator))
            {
                generatorViewUI.SetView(generator.MyData, generator.AmountMultiplier);
                generatorViewUI.gameObject.SetActive(true);
            }
            else
            {
                generatorViewUI.gameObject.SetActive(false);
            }
        }
    }
}