using System;
using DG.Tweening;
using InputSystem;
using Unity.Cinemachine;
using UnityEngine;

namespace _01.Scripts.SJW.Cameras
{
    public class CameraMover : MonoBehaviour
    {
        [SerializeField] private InputControllerSO inputData;
        [SerializeField] private float moveSpeed;
        [SerializeField] private CinemachineCamera targetCam;
        [SerializeField] private Vector2 moveClampSize;
        [SerializeField, Tooltip("min, max")] private Vector2 zoomClampSize;

        private void Awake()
        {
            inputData.OnMoveScrolled += HandleZoomCamera;
        }

        private void OnDestroy()
        {
            inputData.OnMoveScrolled -= HandleZoomCamera;
        }

        private void Update()
        {
            Vector3 pos = transform.position;
            Vector2 dir = inputData.MoveDir;
            
            pos += new Vector3(dir.x, dir.y) * (moveSpeed * Time.deltaTime); 
            
            pos.x = Mathf.Clamp(pos.x, -(moveClampSize.x / 2), (moveClampSize.x / 2));
            pos.y = Mathf.Clamp(pos.y, -(moveClampSize.y / 2), (moveClampSize.y / 2));
            
            targetCam.transform.position = pos;
        }
        
        private void HandleZoomCamera(float value)
        {
            float nextValue =  Mathf.Clamp(targetCam.Lens.OrthographicSize - value*2, zoomClampSize.x, zoomClampSize.y); 
            DOTween.To(()=>targetCam.Lens.OrthographicSize,
                x => targetCam.Lens.OrthographicSize = x,
                nextValue, 
                0.2f)
                .SetEase(Ease.OutCubic);
        }

        #if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(Vector3.zero, moveClampSize);
        }
        #endif
        
    }
}