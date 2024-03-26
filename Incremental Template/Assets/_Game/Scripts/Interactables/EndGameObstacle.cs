using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using MoreMountains.NiceVibrations;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class EndGameObstacle : MonoBehaviour, IDamageable, IInteractable
{
    public float EndGameObstacleNumber;
    [SerializeField] private Transform _moneyTransform;
    [SerializeField] private GameObject _endGameObstacle;
    private bool _isHit;
    private TextMeshPro _numberText;
    private float _startScaleX;

    private void Start()
    {
        _numberText = GetComponentInChildren<TextMeshPro>();
        _numberText.SetText($"{EndGameObstacleNumber}");
        _startScaleX = transform.localScale.x;
    }

    public void TakeBulletDamage(float damageAmount, BulletController bullet)
    {
        MMVibrationManager.Haptic(HapticTypes.SoftImpact);
        transform.DOKill();
        transform.DOScale(_startScaleX + .05f, .07f).OnComplete(() => { transform.DOScale(_startScaleX, .07f); });
        EndGameObstacleNumber -= damageAmount;

        if (EndGameObstacleNumber <= 0)
        {
            gameObject.layer = 0;
            _numberText.gameObject.SetActive(false);
            _endGameObstacle.transform.DOKill();
            _moneyTransform.DOLocalMoveY(transform.position.y + 0.68f, .25f).SetEase(Ease.Linear);
            Destroy(_endGameObstacle);
        }

        _numberText.SetText($"{Mathf.Ceil(EndGameObstacleNumber)}");
    }

    public void InteractPlayer(Transform playerTransform)
    {
        if (_isHit) return;

        var playerController = playerTransform.GetComponent<PlayerController>();
        playerController.Die(0);
        _isHit = true;
    }
}