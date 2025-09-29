using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace InputSystem
{
    [CreateAssetMenu(fileName = "InputData", menuName = "SO/Input", order = 0)]
    public class InputControllerSO : ScriptableObject, Controls.IPlayerActions
    {
        private Controls _control;

        public event Action OnSelectPressed;
        public event Action OnSelectReleased;
        public event Action<float> OnMoveScrolled;
        public event Action OnCancelPressed;
        public event Action OnPausePressed;

        public float ScrollValue { get; private set; }
        public Vector2 MoveDir { get; private set; }
        private Vector2 _screenPointPos;

        private void OnEnable()
        {
            _control ??= new Controls();

            _control.Player.SetCallbacks(this);
            _control.Player.Enable();
        }

        private void OnDisable()
        {
            _control.Player.Disable();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            MoveDir = context.ReadValue<Vector2>().normalized;
        }

        public void OnSelect(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                OnSelectPressed?.Invoke();
            }
            else if (context.canceled)
                OnSelectReleased?.Invoke();
        }

        public Vector2 GetWorldPointPos()
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(_screenPointPos);
            return pos;
        }

        public Vector2 GetScreenPointPos() => _screenPointPos;

        public void OnMousePos(InputAction.CallbackContext context)
        {
            _screenPointPos = context.ReadValue<Vector2>();
        }

        public void OnScroll(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                float value = context.ReadValue<float>();
                OnMoveScrolled?.Invoke(value);
            }
        }

        public void OnCancel(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnCancelPressed?.Invoke();
        }

        public void OnPause(InputAction.CallbackContext context)
        {
            if(context.performed)
                OnPausePressed?.Invoke();
        }

        public void ClearAction()
        {
            OnSelectPressed = null;
            OnSelectReleased = null;
            OnMoveScrolled = null;
            OnCancelPressed = null;
        }
    }
}