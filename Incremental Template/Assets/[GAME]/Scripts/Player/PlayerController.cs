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
    private PlayerAttackController _playerAttackController;

    private void Awake()
    {
        _playerAttackController = GetComponent<PlayerAttackController>();
        _playerMovementController = GetComponent<PlayerMovementController>();
    }

    private void OnEnable()
    {
        EventManager.OnGameStart += StartGame;
        EventManager.OnGateCollect += TakeSkills;
    }

    private void OnDisable()
    {
        EventManager.OnGameStart -= StartGame;
        EventManager.OnGateCollect -= TakeSkills;
    }

    private void Start()
    {
        PlayerState = PlayerStates.Idle;
    }

    private void StartGame()
    {
        PlayerState = PlayerStates.Run;
        _playerAttackController.StartShooting();
        
        var persistData = PersistData.Instance;
        persistData.Save();
        var level = persistData.CurrentLevel;
        Elephant.LevelStarted(level);
    }

    private void TakeSkills(GateController.SkillTypes skillType, float skillAmount)
    {
        _playerAttackController.CalculateSkills(skillType, skillAmount);
    }
}