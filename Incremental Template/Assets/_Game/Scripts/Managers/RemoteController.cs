using System;
using System.Collections;
using System.Collections.Generic;
using SincappStudio;
using UnityEngine;

public class RemoteController : MonoSingleton<RemoteController>
{
    public float[] FireRateCostMultipliers { private set; get; }
    public float[] RangeCostMultipliers { private set; get; }
    public float[] IncomeCostMultipliers { private set; get; }
    
    protected override void Awake()
    {
        base.Awake();
        
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
