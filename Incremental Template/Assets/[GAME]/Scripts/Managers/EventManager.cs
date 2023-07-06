using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

    public static class EventManager
    {
        public static UnityAction OnGameWin;
        public static UnityAction OnGameLose;
        public static UnityAction OnGameStart;
        public static UnityAction OnCollectable;
        public static UnityAction OnIncomeChange;
        public static UnityAction OnIncrementalUpgrade;
        public static UnityAction OnObstacleHit;
        public static UnityAction<GateController.SkillTypes,float> OnGateCollect;
    
}