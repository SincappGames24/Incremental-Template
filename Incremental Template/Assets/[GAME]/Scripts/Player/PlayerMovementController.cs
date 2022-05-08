using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private PlayerSettings _playerSettings;
    [SerializeField] private bool _swerveWithRotation = false;
    private SwerveInputSystem _swerveInputSystem;
    private bool _didIncomeSend;

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
            GetIncome();
        }
    }

    private void SwerveMovement()
    {
        float swerveAmount = Time.deltaTime * _playerSettings.SwerveSpeed * _swerveInputSystem.MoveFactorX;
        transform.Translate(0, 0, (_playerSettings.MovementSpeed * Time.deltaTime), Space.Self);
        transform.Translate(swerveAmount, 0, 0, Space.World);
    }

    private void ClampPosition()
    {
        Vector3 transformPosition = transform.position;
        transformPosition.x = Mathf.Clamp(transformPosition.x, -_playerSettings.MaxSwerveAmount,
            _playerSettings.MaxSwerveAmount);
        transform.position = transformPosition;
    }
    
    private void GetIncome()
    {
        if (!_didIncomeSend)
        {
            if ((transform.position.z) % 1 <= .15f)
            {
                EventManager.OnGetIncome?.Invoke();
                _didIncomeSend = true;
            }
        }
        else
        {
            _didIncomeSend = false;
        }
    }
}