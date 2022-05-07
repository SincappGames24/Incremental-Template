using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

    public static class EventManager
    {
        public static UnityAction GameWin;
        public static UnityAction GameLose;
        public static UnityAction GameStart;
        public static UnityAction<float> CameraShake;
        public static UnityAction OnCollectable;
        public static UnityAction OnObstacleHit;
    
}