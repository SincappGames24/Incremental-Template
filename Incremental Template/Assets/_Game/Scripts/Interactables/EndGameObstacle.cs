using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using MoreMountains.NiceVibrations;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class EndGameObstacle : MonoBehaviour
{
    private const int _playerLayer = 8;
    private const int _bulletLayer = 12;
    private bool _isHit;
    public float EndGameObstacleNumber;
    private TextMeshPro _numberText;
    [SerializeField] private Transform _moneyTransform;
    private float _startScaleX;
    [SerializeField] private GameObject _endGameObstacle;

    private void Start()
    {
        _numberText = GetComponentInChildren<TextMeshPro>();
        _numberText.SetText($"{EndGameObstacleNumber}");
        _startScaleX = transform.localScale.x;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isHit) return;

        if (other.gameObject.layer == _playerLayer)
        {
            var playerController = other.GetComponent<PlayerController>();
            playerController.Die(0);
            _isHit = true;
        }

        else if (other.gameObject.layer == _bulletLayer)
        {
            MMVibrationManager.Haptic(HapticTypes.SoftImpact);
            transform.DOKill();
            transform.DOScale(_startScaleX + .05f, .07f).OnComplete(() => { transform.DOScale(_startScaleX, .07f); });
            EndGameObstacleNumber -= PersistData.Instance.BulletPower;

            if (EndGameObstacleNumber <= 0)
            {
                gameObject.layer = 0;
                _numberText.gameObject.SetActive(false);
                _endGameObstacle.transform.DOKill();
                _moneyTransform.DOLocalMoveY(transform.position.y + 0.68f, .25f).SetEase(Ease.Linear);
                Destroy(_endGameObstacle);
            }

            _numberText.SetText($"{Mathf.Ceil(EndGameObstacleNumber)}");
            other.gameObject.layer = 0;
            other.transform.DOKill();
            Destroy(other.gameObject);
        }
    }
}