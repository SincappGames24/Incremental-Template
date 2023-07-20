using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.NiceVibrations;
using UnityEngine;
using UnityEngine.UI;

public class IncrementalManager : MonoBehaviour
{
    public void IncreaseFireRate()
    {
        var persistData = PersistData.Instance;

        Purchase(persistData.MaxFireRateCost, ref persistData.FireRateUpgradeCost, ref persistData.FireRateLevel);
        Upgrade(ref persistData.FireRate, -0.01f);
    }

    public void IncreaseRange()
    {
        var persistData = PersistData.Instance;

        Purchase(persistData.MaxRangeCost, ref persistData.RangeUpgradeCost, ref persistData.RangeLevel);
        Upgrade(ref persistData.Range, 1);
    }

    public void IncreaseIncome()
    {
        var persistData = PersistData.Instance;

        Purchase(persistData.MaxIncomeCost, ref persistData.IncomeUpgradeCost, ref persistData.IncomeLevel);
        Upgrade(ref persistData.Income, 1);
    }

    private void Purchase(int costLimit, ref float currentCost, ref int currentLevel)
    {
        MMVibrationManager.Haptic(HapticTypes.MediumImpact);
        var persistData = PersistData.Instance;
        currentLevel++;
        persistData.Money -= currentCost;
        
        var costMultiplier = 1.2f;

        if (currentCost > 200)
        {
            costMultiplier = 1.07f;
        }
        
        else if (currentCost > 500)
        {
            costMultiplier = 1.05f;
        }
        
        else if (currentCost > 1000)
        {
            costMultiplier = 1.02f;
        }
        
        else if (currentCost > 20000)
        {
            costMultiplier = 1.01f;
        }

        if (currentCost < costLimit)
        {
            currentCost *= costMultiplier;
        }
    }

    private void Upgrade(ref float incremental, float incrementAmount)
    {
        var persistData = PersistData.Instance;

        incremental += incrementAmount;
        EventManager.OnIncrementalUpgrade?.Invoke();
        persistData.Save();
    }
}