using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.NiceVibrations;
using UnityEngine;
using UnityEngine.UI;

public class IncrementalManager : MonoBehaviour
{
    public void IncreaseStamina()
    {
        var persistData = PersistData.Instance;

        Purchase(persistData.MaxStaminaCost, ref persistData.StaminaUpgradeCost, ref persistData.StaminaLevel);
        Upgrade(ref persistData.Stamina, 1);
    }

    public void IncreaseSpeed()
    {
        var persistData = PersistData.Instance;

        Purchase(persistData.MaxSpeedCost, ref persistData.SpeedUpgradeCost, ref persistData.SpeedLevel);
        Upgrade(ref persistData.Speed, 1);
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
        currentCost *= 2;
        currentCost = Mathf.Clamp(currentCost, 0, costLimit);
    }

    private void Upgrade(ref float incremental, float incrementAmount)
    {
        var persistData = PersistData.Instance;

        incremental += incrementAmount;
        EventManager.OnIncrementalUpgrade?.Invoke();
        persistData.Save();
    }
}