using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using MoreMountains.NiceVibrations;
using UnityEngine;

public class GateGroupController : Movementable
{
    private const int _playerLayer = 8;
    private const int _bulletLayer = 12; 
    private bool _isHit;
    private bool _isSingleGate;

    private void Awake()
    {
        if (GetComponentsInChildren<GateController>().Length == 1)
        {
            _isSingleGate = true;
        }

        if (_movementType == MovementHelper.MovementTypes.Horizontal || _movementType == MovementHelper.MovementTypes.PingPong)
        {
            MovementHelper.SetMovement(transform, _movementType, _movementSpeed, _horizontalMoveOffset,
                _verticalMoveOffset, _pingPongOffset);
            IsMoving = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GateController hittedGate = FindInteractedGate(other);

        if (other.gameObject.layer == _playerLayer)
        {
            hittedGate.UseSkill();
            _isHit = true;
        }

        if (other.gameObject.layer == _bulletLayer)
        {
            hittedGate.IncreaseSkillAmountOnBulletHit();
            Move();
            other.transform.DOKill();
            Destroy(other.gameObject);
        }
    }

    private GateController FindInteractedGate(Collider other)
    {
        if (!_isSingleGate)
        {
            if (other.gameObject.transform.position.x < 0)
            {
                return transform.GetChild(0).GetComponent<GateController>();
            }
           
            return transform.GetChild(1).GetComponent<GateController>();
        }

        return transform.GetChild(0).GetComponent<GateController>();
    }

    private void Move()
    {
        if (!IsMoving)
        {
            MovementHelper.SetMovement(transform, _movementType, _movementSpeed, _horizontalMoveOffset, _verticalMoveOffset, _pingPongOffset);
            IsMoving = true;
        }
    }
}