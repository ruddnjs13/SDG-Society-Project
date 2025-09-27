using System;
using InputSystem;
using UnityEngine;

namespace _01.Scripts.SJW.Cameras
{
    public class CameraMover : MonoBehaviour
    {
        [SerializeField] private InputControllerSO inputData;
        [SerializeField] private float moveSpeed;
        [SerializeField] private Transform target;
        [SerializeField] private Vector2 clampSize;
        
        private void Update()
        {
            Vector3 pos = transform.position;
            Vector2 dir = inputData.MoveDir;
            
            pos += new Vector3(dir.x, dir.y) * (moveSpeed * Time.deltaTime); 
            
            pos.x = Mathf.Clamp(pos.x, -(clampSize.x / 2), (clampSize.x / 2));
            pos.y = Mathf.Clamp(pos.y, -(clampSize.y / 2), (clampSize.y / 2));
            
            transform.position = pos;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(Vector3.zero, clampSize);
        }
    }
}