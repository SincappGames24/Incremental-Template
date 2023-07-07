using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CollectableController : MonoBehaviour
{
    private const int _playerLayer = 8;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == _playerLayer)
        {
            EventManager.OnCollectable?.Invoke();
            Destroy(gameObject);
        }
    }
}