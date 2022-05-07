using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class CameraController : MonoBehaviour
    {
        [SerializeField] private CameraSettings _cameraSettings;
        [SerializeField] private Transform _playerTransform;

        private void LateUpdate()
        {
            transform.position = _playerTransform.position + _cameraSettings.CameraOffset;
        }
    }


