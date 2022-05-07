using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [Tooltip("Rotasyonlu Swerve Movement")] [SerializeField]
    private PlayerSettings _playerSettings;
    [SerializeField] private bool _swerveWithRotation = false;
    [ConditionalHide("_swerveWithRotation", true)] [SerializeField]
    private float _rotationAngle;
    private SwerveInputSystem _swerveInputSystem;

    private void Awake()
    {
        _swerveInputSystem = FindObjectOfType<SwerveInputSystem>();
    }

    private void Update()
    {
        if (PlayerController.PlayerState == PlayerController.PlayerStates.Run)
        {
            SwerveMovement();
            ClampPosition();

            if (_swerveWithRotation)
            {
                if (CanRotate())
                {
                    RotationMovement();
                }
            }
        }
    }

    private void SwerveMovement()
    {
        float swerveAmount = Time.deltaTime * _playerSettings.SwerveSpeed * _swerveInputSystem.MoveFactorX;
        transform.Translate(0, 0, (_playerSettings.MovementSpeed * Time.deltaTime), Space.Self);
        transform.Translate(swerveAmount, 0, 0, Space.World);

        if (swerveAmount == 0 )
        {
            ResetRotation();
        }
    }

    private void ClampPosition()
    {
        Vector3 transformPosition = transform.position;
        transformPosition.x = Mathf.Clamp(transformPosition.x, -_playerSettings.MaxSwerveAmount,
            _playerSettings.MaxSwerveAmount);
        transform.position = transformPosition;
    }

    private bool CanRotate()
    {
        if (!Input.GetMouseButton(0))
        {
            ResetRotation();
        }

        if (_playerSettings.MaxSwerveAmount - Mathf.Abs(transform.localPosition.x) < float.Epsilon)
        {
            ResetRotation();
            return false;
        }

        return true;
    }

    private void RotationMovement()
    {
        float rotationMagnitude =
            _swerveInputSystem.MoveFactorX * Time.deltaTime * _playerSettings.RotationSpeed;
        float rotationDirection = Mathf.Sign(_swerveInputSystem.MoveFactorX);
        transform.localRotation = Quaternion.Lerp(transform.localRotation,
            Quaternion.Euler(0, _rotationAngle * rotationDirection, 0), Mathf.Abs(rotationMagnitude));
    }

    private void ResetRotation() => transform.localRotation =
        Quaternion.Lerp(transform.localRotation, Quaternion.identity, Time.fixedDeltaTime * 10);
}