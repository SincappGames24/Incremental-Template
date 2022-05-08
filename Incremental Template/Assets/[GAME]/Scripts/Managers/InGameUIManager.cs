using System;
using System.Collections;
using System.Collections.Generic;
using SincappStudio;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class InGameUIManager : MonoBehaviour
{
    [SerializeField] private GameObject _tapToStart;
    [SerializeField] private TextMeshProUGUI _playerMoney;
    private Animator _animator;

    #region Incrementals

    [Header("Incrementals")]
    [SerializeField] private GameObject _incrementalsObj;
    [SerializeField] private TextMeshProUGUI _staminaUpgradeMoney;
    [SerializeField] private TextMeshProUGUI _staminaUpgradeLevel;
    [SerializeField] private TextMeshProUGUI _speedUpgradeMoney;
    [SerializeField] private TextMeshProUGUI _speedUpgradeLevel;
    [SerializeField] private TextMeshProUGUI _incomeUpgradeMoney;
    [SerializeField] private TextMeshProUGUI _incomeUpgradeLevel;

    #endregion

    #region ProgressBar

    [Header("Progress")]
    [SerializeField] private Image _fillbar;
    [SerializeField] private Image _playerMarker;
    [SerializeField] private TextMeshProUGUI _levelText;
    private Transform _playerPosition;
    private Transform _finishPosition;
    private float _playerStartPos_Z;
    private float _totalDistance;

    #endregion
  

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _playerPosition = FindObjectOfType<PlayerController>().transform;
        _finishPosition = GameObject.FindGameObjectWithTag("Finish").transform;
        _levelText.SetText($"{PersistData.Instance.CurrentLevel - 1}");
        _playerStartPos_Z = _playerPosition.position.z;
        _totalDistance = Mathf.Abs(_playerStartPos_Z - _finishPosition.position.z);
        SetIncrementalUI();
    }

    private void OnEnable()
    {
        EventManager.GameStart += GameStarted;
        EventManager.OnIncrementalUpgrade += SetIncrementalUI;
        EventManager.OnGetIncome += UpdateMoneyText;
        EventManager.OnGetIncome += FadeMoney;
        EventManager.GameWin += DisableInGameUI;
        EventManager.GameLose += DisableInGameUI;
    }

    private void OnDisable()
    {
        EventManager.GameStart -= GameStarted;
        EventManager.OnIncrementalUpgrade -= SetIncrementalUI;
        EventManager.OnGetIncome -= UpdateMoneyText;
        EventManager.OnGetIncome -= FadeMoney;
        EventManager.GameWin -= DisableInGameUI;
        EventManager.GameLose -= DisableInGameUI;
    }

    private void Update()
    {
       float fillAmount = Mathf.Clamp((-1 * _playerStartPos_Z + _playerPosition.position.z) / (_totalDistance), 0.0f, 1.0f);
       _fillbar.fillAmount = fillAmount;
        // Vector2 playerMarkerPos = new Vector2((_fillbar.rectTransform.sizeDelta.x * fillAmount) + _fillbar.rectTransform.anchoredPosition.x, _playerMarker.rectTransform.anchoredPosition.y);
        // _playerMarker.rectTransform.anchoredPosition = playerMarkerPos;
    }

    private void GameStarted()
    {
        _tapToStart.SetActive(false);
        _incrementalsObj.SetActive(false);
    }
    
    private void UpdateMoneyText()
    {
        _playerMoney.SetText((PersistData.Instance.Money).ToString("f0"));
    }

    private void SetIncrementalUI()
    {
        StartCoroutine(Sincapp.WaitAndAction(0, () =>
        {
            var persistData = PersistData.Instance;
            _playerMoney.SetText(persistData.Money.ToString("0"));
            
            _staminaUpgradeMoney.SetText($"${persistData.StaminaUpgradeCost}");
            _speedUpgradeMoney.SetText($"${persistData.SpeedUpgradeCost}");
            _incomeUpgradeMoney.SetText($"${persistData.IncomeUpgradeCost}");
            
            _incomeUpgradeLevel.SetText($"{persistData.IncomeLevel} LVL");
            _staminaUpgradeLevel.SetText($"{persistData.StaminaLevel} LVL");
            _speedUpgradeLevel.SetText($"{persistData.SpeedLevel} LVL");
        }));
    }
    
    private void FadeMoney()
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("MoneyFade")) return;
        
        _animator.Play("MoneyFade");
    }

    private void DisableInGameUI()
    {
        gameObject.SetActive(false);
    }
}