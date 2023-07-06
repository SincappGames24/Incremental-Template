using System;
using System.Collections;
using System.Collections.Generic;
using ElephantSDK;
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
    }

    private void OnDisable()
    {
        EventManager.OnGameStart -= StartGame;
    }

    private void Start()
    {
        _playerMovementController = GetComponent<PlayerMovementController>();
        PlayerState = PlayerStates.Idle;
    }

    private void StartGame()
    {
        PlayerState = PlayerStates.Run;
        
        var persistData = PersistData.Instance;
        persistData.Save();
        var level = persistData.CurrentLevel;
        Elephant.LevelStarted(level);
    }
}