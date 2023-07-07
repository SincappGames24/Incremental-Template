using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    private const int _playerLayer = 8;
    private int _counter;
    private int _bulletCounter;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == _playerLayer)
        {
            other.GetComponent<PlayerMovementController>().BounceBackGate();

            _counter++;
         
            if (_counter == 3)
            {
                Destroy(gameObject);
            }
        }
    }
}