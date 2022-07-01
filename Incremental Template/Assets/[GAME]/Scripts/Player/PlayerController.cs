using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum PlayerStates
    {
        Idle,
        Run,
        Dead,
        OnFinishWall,
        Finish
    }

    public static PlayerStates PlayerState;
    private PlayerMovementController _playerMovementController;

    private void OnEnable()
    {
        EventManager.OnGameStart += StartGame;
        EventManager.OnGetIncome += GetIncome;
    }

    private void OnDisable()
    {
        EventManager.OnGameStart -= StartGame;
        EventManager.OnGetIncome -= GetIncome;
    }

    private void Start()
    {
        _playerMovementController = GetComponent<PlayerMovementController>();
        PlayerState = PlayerStates.Idle;
    }

    private void StartGame()
    {
        PlayerState = PlayerStates.Run;
    }
    
    private void GetIncome()
    {
        var persistData = PersistData.Instance;
        persistData.Money += persistData.Income;
    }
}