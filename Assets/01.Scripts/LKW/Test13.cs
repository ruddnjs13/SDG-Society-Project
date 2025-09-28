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
    [SerializeField] private GameEventChannelSO environmentChannel;
    [SerializeField] private GameObject generatorItem;
    [SerializeField] private GeneratorDataSO generatorData;
    [SerializeField] private WeatherType weatherType;
    [SerializeField] private TimeZoneType timeZoneType;

    private void Update()
    {
        if (Keyboard.current.qKey.wasPressedThisFrame)
        {
            Generator generator =  Instantiate(generatorItem, transform.position, Quaternion.identity)
                .GetComponent<Generator>();
            generator.Initialize(generatorData);
        }
        
        if (Keyboard.current.f1Key.wasPressedThisFrame)
        {
            var evt = EnvironmentEvents.EnvironmentChangeEvent.Init(new SendEnvironmentData()
                {
                    TypeBit = (int)weatherType | (int)timeZoneType
                }
                , null);
            environmentChannel.RaiseEvent(evt);
        }
    }
}
