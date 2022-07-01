using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IncrementalManager : MonoBehaviour
{
    public void IncreaseStamina()
    {
        var persistData = PersistData.Instance;

        if (!CheckMoneyAmount(persistData.StaminaUpgradeCost)) return;

        if (!CheckLevelLimit(persistData.StaminaLevel, persistData.MaxStaminaLevel)) return;

        Purchase(persistData.MaxStaminaCost, ref persistData.StaminaUpgradeCost, ref persistData.StaminaLevel);
        Upgrade(ref persistData.Stamina, 1);
    }

    public void IncreaseSpeed()
    {
        var persistData = PersistData.Instance;

        if (!CheckMoneyAmount(persistData.SpeedUpgradeCost)) return;

        if (!CheckLevelLimit(persistData.SpeedLevel, persistData.MaxSpeedLevel)) return;

        Purchase(persistData.MaxSpeedCost, ref persistData.SpeedUpgradeCost, ref persistData.SpeedLevel);
        Upgrade(ref persistData.Speed, 1);
    }

    public void IncreaseIncome()
    {
        var persistData = PersistData.Instance;

        if (!CheckMoneyAmount(persistData.IncomeUpgradeCost)) return;

        if (!CheckLevelLimit(persistData.IncomeLevel, persistData.MaxIncomeLevel)) return;

        Purchase(persistData.MaxIncomeCost, ref persistData.IncomeUpgradeCost, ref persistData.IncomeLevel);
        Upgrade(ref persistData.Income, 1);
    }

    private bool CheckMoneyAmount(int cost)
    {
        var persistData = PersistData.Instance;

        return persistData.Money >= cost;
    }

    private bool CheckLevelLimit(int currentLevel, int maxLevel)
    {
        return currentLevel < maxLevel;
    }

    private void Purchase(int costLimit, ref int currentCost, ref int currentLevel)
    {
        var persistData = PersistData.Instance;
        currentLevel++;
        persistData.Money -= currentCost;

        if (currentCost < costLimit)
        {
            currentCost *= 2;
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