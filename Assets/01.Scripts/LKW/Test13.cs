using System;
using Code.Events;
using Code.Weathers;
using Core.GameEvent;
using LKW.Generators;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test13 : MonoBehaviour
{
    private void Update()
    {
        if (Keyboard.current.f1Key.wasPressedThisFrame)
        {
            Time.timeScale = 5f;
        }
        if (Keyboard.current.f2Key.wasPressedThisFrame)
        {
            Time.timeScale = 1f;
        }
    }
}
