using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SpeedPathController : MonoBehaviour
{
    [SerializeField] private SpeedDirections _speedDirection;
    public float _forwardSpeedBoostMultiplier = 1.5f;
    public float _backwardSpeedBoostMultiplier = .5f;
    [SerializeField] private MeshRenderer _arrowMesh;
    private const int _playerLayer = 8;
    private bool _isHit;

    public enum SpeedDirections
    {
        Forward,
        Backward
    }

    private void Start()
    {
        if (_speedDirection == SpeedDirections.Backward)
        {
            _arrowMesh.material.color = new Color(0.78f, 0.24f, 0.24f);
            _arrowMesh.transform.localRotation = Quaternion.Euler(0,180,0);
        }
        
        _arrowMesh.material.DOOffset(new Vector2(0, -1), 0.65f).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);
    }

    private void OnTriggerEnter(Collider other)
    {
        float speedMultiplier = 1.0f;
        
        if (other.gameObject.layer == _playerLayer)
        {
            if (_speedDirection == SpeedDirections.Backward)
            {
                speedMultiplier = _backwardSpeedBoostMultiplier;
            }

            if (_speedDirection == SpeedDirections.Forward)
            {
                speedMultiplier = _forwardSpeedBoostMultiplier;
            }

            other.GetComponent<PlayerMovementController>().SetSpeedBoost(speedMultiplier);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == _playerLayer)
        {
            other.GetComponent<PlayerMovementController>().SetSpeedBoost(1.0f);
        }
    }
}