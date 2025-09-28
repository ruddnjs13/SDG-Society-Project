using System;
using InputSystem;
using LKW.Generators;
using UnityEngine;

namespace LKW.Players
{
    public class PlayerController : MonoBehaviour
    {
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
            Vector3 origin = inputController.GetScreenPointPos();

            if (Physics.Raycast(origin, Vector3.forward, out RaycastHit hit, Mathf.Infinity,
                    LayerMask.GetMask("Building")))
            {
                if (hit.collider != null && hit.collider.TryGetComponent(out Generator generator))
                {
                    
                }
            }
        }
    }
}