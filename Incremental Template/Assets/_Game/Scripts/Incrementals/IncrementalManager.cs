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
        
        #region MultiplierIndexCalculation

        int multiplierIndex = 0;
          
        if (persistData.FireRateLevel > 0 && persistData.FireRateLevel < 10)
        {
            multiplierIndex = 0;
        }
        else  if (persistData.FireRateLevel >= 10 && persistData.FireRateLevel < 20)
        {
            multiplierIndex = 1;
        }
        else  if (persistData.FireRateLevel >= 20 && persistData.FireRateLevel < 50)
        {
            multiplierIndex = 2;
        }
        else  if (persistData.FireRateLevel >= 50 && persistData.FireRateLevel <= 200)
        {
            multiplierIndex = 3;
        }
        else  if (persistData.FireRateLevel > 200 && persistData.FireRateLevel <= 250)
        {
            multiplierIndex = 4;
        }
        else
        {
            multiplierIndex = 5;
        }

        #endregion
        persistData.FireRateUpgradeCost *= RemoteController.Instance.FireRateCostMultipliers[multiplierIndex];
        EventManager.OnIncrementalUpgrade?.Invoke();
    }

    public void IncreaseRange()
    {
        var persistData = PersistData.Instance;

        Purchase(persistData.MaxRangeCost, ref persistData.RangeUpgradeCost, ref persistData.RangeLevel);
        Upgrade(ref persistData.Range, 1);
        
        #region MultiplierIndexCalculation

        int multiplierIndex = 0;
          
        if (persistData.RangeLevel > 0 && persistData.RangeLevel < 10)
        {
            multiplierIndex = 0;
        }
        else  if (persistData.RangeLevel >= 10 && persistData.RangeLevel < 20)
        {
            multiplierIndex = 1;
        }
        else  if (persistData.RangeLevel >= 20 && persistData.RangeLevel < 50)
        {
            multiplierIndex = 2;
        }
        else  if (persistData.RangeLevel >= 50 && persistData.RangeLevel <= 200)
        {
            multiplierIndex = 3;
        }
        else  if (persistData.RangeLevel > 200 && persistData.RangeLevel <= 250)
        {
            multiplierIndex = 4;
        }
        else
        {
            multiplierIndex = 5;
        }

        #endregion
        persistData.RangeUpgradeCost *= RemoteController.Instance.RangeCostMultipliers[multiplierIndex];
        EventManager.OnIncrementalUpgrade?.Invoke();
    }

    public void IncreaseIncome()
    {
        var persistData = PersistData.Instance;

        Purchase(persistData.MaxIncomeCost, ref persistData.IncomeUpgradeCost, ref persistData.IncomeLevel);
        Upgrade(ref persistData.Income, 1);
        
        #region MultiplierIndexCalculation

        int multiplierIndex = 0;
          
        if (persistData.IncomeLevel > 0 && persistData.IncomeLevel < 10)
        {
            multiplierIndex = 0;
        }
        else  if (persistData.IncomeLevel >= 10 && persistData.IncomeLevel < 20)
        {
            multiplierIndex = 1;
        }
        else  if (persistData.IncomeLevel >= 20 && persistData.IncomeLevel < 50)
        {
            multiplierIndex = 2;
        }
        else  if (persistData.IncomeLevel >= 50 && persistData.IncomeLevel <= 200)
        {
            multiplierIndex = 3;
        }
        else  if (persistData.IncomeLevel > 200 && persistData.IncomeLevel <= 250)
        {
            multiplierIndex = 4;
        }
        else
        {
            multiplierIndex = 5;
        }

        #endregion
        persistData.IncomeUpgradeCost *= RemoteController.Instance.IncomeCostMultipliers[multiplierIndex];
        EventManager.OnIncrementalUpgrade?.Invoke();
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