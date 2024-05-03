using System;
using System.Collections;
using System.Collections.Generic;
using ElephantSDK;
using SincappStudio;
using UnityEngine;

public class RemoteController : MonoSingleton<RemoteController>
{
    public float[] FireRateCostMultipliers { private set; get; }
    public float[] RangeCostMultipliers { private set; get; }
    public float[] IncomeCostMultipliers { private set; get; }
    public float[] EndGameObstacleNumbers { private set; get; }
    public float[] EndGameObstacleMultipliersWhenReached { get; set; }
    public float FireRateGateCollectLerpMax;
    public float RangeGateCollectLerpMax;
    public bool DebugPanelActive;

    protected override void Awake()
    {
        base.Awake();
        
        EndGameObstacleNumbers = Sincapp.StringListToFloatArray("End_Game_Obstacle_Numbers",
            "5-10-15-25-50-80-100-150-200-250-300-350-500-550-600-650-700-750-800-1000-1000-1000-1000-1000-1000-1000-1000-1200-1400");
        
        FireRateGateCollectLerpMax = RemoteConfig.GetInstance().GetFloat("FireRateGateCollectLerpMax", 100f);
        RangeGateCollectLerpMax = RemoteConfig.GetInstance().GetFloat("RangeGateCollectLerpMax", 100f);
        EndGameObstacleMultipliersWhenReached = Sincapp.StringListToFloatArray("EndGameObstacleMultipliersWhenReached",
            "1-1.35-3.5-10-15-20");
        
          
        DebugPanelActive =
            RemoteConfig.GetInstance().GetBool("DebugPanelActive", Application.isEditor);
        
        #region Economy

        FireRateCostMultipliers =
            Sincapp.StringListToFloatArray("FireRate_Cost_Multipliers", "1.35-1.25-1.02-1.01-1.001-1.001");
        RangeCostMultipliers =
            Sincapp.StringListToFloatArray("Range_Cost_Multipliers", "1.35-1.2-1.02-1.01-1.001-1.001");
        IncomeCostMultipliers =
            Sincapp.StringListToFloatArray("Income_Cost_Multipliers", "1.35-1.25-1.05-1.01-1.001-1.001");

        #endregion
    }
}
