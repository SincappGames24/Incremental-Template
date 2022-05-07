using System;
using System.Collections;
using System.Collections.Generic;
using SincappStudio;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


    public class EndGameUIManager : MonoBehaviour
    {
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _restartButton;
        [SerializeField] private TextMeshProUGUI _giftPercentText;
        [SerializeField] private Image _giftFillImage;
        [SerializeField] private GameObject _continueButtonGift;
        [SerializeField] private GameObject _firfir;

        private Animator _animator;
        private float _giftFillSpeed = 0.15f;
        private float _giftPercent;
        private bool _loadGift;
        private readonly int _gift = Animator.StringToHash("OpenGift");
        private readonly int _restartUI = Animator.StringToHash("OpenRestartUI");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            EventManager.GameWin += OpenGift;
            EventManager.GameLose += OpenRestartUI;
            _continueButton.onClick.AddListener(GameManager.Instance.NextLevel);
            _restartButton.onClick.AddListener(GameManager.Instance.RestartLevel);
        }

        private void OnDisable()
        {
            EventManager.GameWin -= OpenGift;
            EventManager.GameLose -= OpenRestartUI;
            _continueButton.onClick.RemoveAllListeners();
            _restartButton.onClick.RemoveAllListeners();
        }

        private void Start()
        {
            var persistData = PersistData.Instance;

            if (persistData.GiftFillAmount >= 1.0f)
            {
                persistData.GiftFillAmount = 0f;
            }

            _giftFillImage.fillAmount = persistData.GiftFillAmount;
            _giftPercent = _giftFillImage.fillAmount * 100;
            _giftPercentText.SetText($"%{_giftPercent:F0}");
        }

        private void Update()
        {
            var persistData = PersistData.Instance;

            if (_loadGift)
            {
                _giftFillImage.fillAmount = Mathf.Clamp(_giftFillImage.fillAmount += _giftFillSpeed * Time.deltaTime, 0,
                    persistData.GiftFillAmount);
                _giftPercent = _giftFillImage.fillAmount * 100;
                _giftPercentText.SetText($"%{_giftPercent:F0}");

                if (persistData.GiftFillAmount - _giftFillImage.fillAmount < 0.001f)
                {
                    StartCoroutine(Sincapp.WaitAndAction(0.5f, () => _continueButtonGift.SetActive(true)));
                    _firfir.SetActive(true);
                    _loadGift = false;
                }
            }
        }

        private void OpenGift()
        {
            _animator.SetTrigger(_gift);
        }

        private void SetFillAmount() // Animation Event
        {
            var persistData = PersistData.Instance;

            if (!_loadGift)
            {
                persistData.GiftFillAmount += .1f;
                _loadGift = true;
            }
        }

        private void OpenRestartUI() => _animator.SetTrigger(_restartUI);
    }
