using System;
using LKW.Generators;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test13 : MonoBehaviour
{
    [SerializeField] private GameObject generatorItem;
    [SerializeField] private GeneratorDataSO generatorData;

    private void Update()
    {
        if (Keyboard.current.qKey.wasPressedThisFrame)
        {
            Generator generator =  Instantiate(generatorItem, transform.position, Quaternion.identity)
                .GetComponent<Generator>();
            generator.Initialize(generatorData);
        }
    }
}
