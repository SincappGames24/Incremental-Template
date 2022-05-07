using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;


    public class Obstacles : MonoBehaviour
    {
        private const int _playerLayer = 8;

        private void OnTriggerEnter(Collider other)
        {
            EventManager.OnObstacleHit?.Invoke();
        }
    }
