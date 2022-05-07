using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    [CreateAssetMenu(fileName = "PlayerSettings", menuName = "ScriptableObjects/PlayerSettings")]
    public class PlayerSettings : ScriptableObject
    {

        [SerializeField] private float _speed;
        [SerializeField] private float _swerveSpeed;
        [SerializeField] private float _maxSwerveAmount;
        [SerializeField] private float _rotationSpeed;

        public float MovementSpeed => _speed;
        public float SwerveSpeed => _swerveSpeed;
        public float RotationSpeed => _rotationSpeed;
        public float MaxSwerveAmount => _maxSwerveAmount;
    }
