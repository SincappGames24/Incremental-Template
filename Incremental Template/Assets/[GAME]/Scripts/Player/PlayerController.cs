using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum PlayerStates
    {
        //Idle,
        Run,
        Dead,
        OnFinishWall,
        Finish
    }

    public static PlayerStates PlayerState;
    private PlayerMoneyController _playerMoneyController;

    private void Start()
    {
        _playerMoneyController = GetComponent<PlayerMoneyController>();
    }

    private void IncreaseMoney()
    {
        _playerMoneyController.IncreaseMoney(50);
    }

    private void DecreaseMoney()
    {
        _playerMoneyController.DecreaseMoney(50);
    }
}