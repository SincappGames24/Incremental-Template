using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    [CreateAssetMenu(fileName = "Camera Settings", menuName = "ScriptableObjects/Camera")]
    public class CameraSettings : ScriptableObject
    {
        [SerializeField] private Vector3 _cameraOffset;
        public Vector3 CameraOffset => _cameraOffset;
    }

