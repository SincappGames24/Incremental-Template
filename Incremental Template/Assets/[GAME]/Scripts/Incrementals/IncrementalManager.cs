using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IncrementalManager : MonoBehaviour
{
    [SerializeField] private Sprite _staminaNormalSprite;
    [SerializeField] private Sprite _staminaNoMoneySprite;
    [SerializeField] private Sprite _incomeNormalSprite;
    [SerializeField] private Sprite _incomeNoMoneySprite;
    [SerializeField] private Sprite _speedNormalSprite;
    [SerializeField] private Sprite _speedNoMoneySprite;
    [SerializeField] private Button _ageButton;
    [SerializeField] private Button _incomeButton;
    [SerializeField] private Button _speedButton;

    private void Start()
    {
        CheckMoneySprites();
    }

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
        
        CheckMoneySprites();
    }

    private void Upgrade(ref float incremental, float incrementAmount)
    {
        var persistData = PersistData.Instance;

        incremental += incrementAmount;
        EventManager.OnIncrementalUpgrade?.Invoke();
        persistData.Save();
    }

    private void CheckMoneySprites()
    {
        var persistData = PersistData.Instance;

        if (persistData.StaminaLevel < persistData.MaxStaminaLevel &&
            persistData.Money >= persistData.StaminaUpgradeCost)
        {
            _ageButton.image.sprite = _staminaNormalSprite;
            _ageButton.interactable = true;
        }
        else
        {
            _ageButton.image.sprite = _staminaNoMoneySprite;
            _ageButton.interactable = false;
        }

        if (persistData.IncomeLevel < persistData.MaxIncomeLevel &&
            persistData.Money >= persistData.IncomeUpgradeCost)
        {
            _incomeButton.image.sprite = _incomeNormalSprite;
            _incomeButton.interactable = true;
        }
        else
        {
            _incomeButton.image.sprite = _incomeNoMoneySprite;
            _incomeButton.interactable = false;
        }

        if (persistData.SpeedLevel < persistData.MaxSpeedLevel &&
            persistData.Money >= persistData.SpeedUpgradeCost)
        {
            _speedButton.image.sprite = _speedNormalSprite;
            _speedButton.interactable = true;
        }
        else
        {
            _speedButton.image.sprite = _speedNoMoneySprite;
            _speedButton.interactable = false;
        }
    }
}