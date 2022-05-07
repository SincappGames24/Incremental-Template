using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


    public class InGameUIManager : MonoBehaviour
    {
        [SerializeField] private Transform _playerPosition;
        [SerializeField] private Transform _finishPosition;
        [SerializeField] private Image _fillbar;
        [SerializeField] private Image _playerMarker;
        [SerializeField] private TextMeshProUGUI _levelText;
        [SerializeField] private GameObject _tapToStart;
        private float _playerStartPos_Z;
        private float _totalDistance;

        private void Start()
        {
            _levelText.SetText($"LEVEL {PersistData.Instance.CurrentLevel - 1}");
            _playerStartPos_Z = _playerPosition.position.z;
            _totalDistance = Mathf.Abs(_playerStartPos_Z - _finishPosition.position.z);
        }

        private void OnEnable()
        {
            EventManager.GameStart += GameStarted;
            EventManager.GameWin += DisableInGameUI;
            EventManager.GameLose += DisableInGameUI;
        }

        private void OnDisable()
        {
            EventManager.GameStart -= GameStarted;
            EventManager.GameWin -= DisableInGameUI;
            EventManager.GameLose -= DisableInGameUI;
        }

        private void Update()
        {
            float fillAmount = Mathf.Clamp((-1 * _playerStartPos_Z + _playerPosition.position.z) / (_totalDistance),
                0.0f,
                1.0f);
            _fillbar.fillAmount = fillAmount;
            Vector2 playerMarkerPos =
                new Vector2(
                    (_fillbar.rectTransform.sizeDelta.x * fillAmount) + _fillbar.rectTransform.anchoredPosition.x,
                    _playerMarker.rectTransform.anchoredPosition.y);
            _playerMarker.rectTransform.anchoredPosition = playerMarkerPos;
        }

        private void GameStarted()
        {
            _tapToStart.SetActive(false);
        }

        private void DisableInGameUI()
        {
            gameObject.SetActive(false);
        }
    }
