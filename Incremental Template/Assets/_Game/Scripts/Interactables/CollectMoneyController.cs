using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectMoneyController : MonoBehaviour
{
    private bool _isHit;
    private const int _playerLayer = 8;

    private void OnTriggerEnter(Collider other)
    {
        if (_isHit) return;

        PersistData persistData = PersistData.Instance;

        if (other.gameObject.layer == _playerLayer)
        {
            other.GetComponent<PlayerController>().CollectMoney(persistData.Income);
            ParticleManager.Instance.MoneyParticle(transform.position + Vector3.up * 1f);
            Destroy(gameObject);
            _isHit = true;
        }
    }
}