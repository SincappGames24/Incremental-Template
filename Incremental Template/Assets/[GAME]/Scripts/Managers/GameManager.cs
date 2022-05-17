using System.Collections;
using System.Collections.Generic;
using ElephantSDK;
//using GameAnalyticsSDK;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

    public class GameManager : MonoSingleton<GameManager> //, IGameAnalyticsATTListener 
    {
        private bool _isGameStarted;

        private void Awake()
        {
            Input.multiTouchEnabled = false;
            var level = PersistData.Instance.CurrentLevel;
            Elephant.LevelStarted(level);
            Application.targetFrameRate = 60;
        }

        #region GameAnalitcs SDK

        // void Start()
        // {
        //     if (Application.platform == RuntimePlatform.IPhonePlayer)
        //     {
        //         GameAnalytics.RequestTrackingAuthorization(this);
        //     }
        //     else
        //     {
        //         GameAnalytics.Initialize();
        //     }
        // }
        //
        // public void GameAnalyticsATTListenerNotDetermined()
        // {
        //     GameAnalytics.Initialize();
        // }
        //
        // public void GameAnalyticsATTListenerRestricted()
        // {
        //     GameAnalytics.Initialize();
        // }
        //
        // public void GameAnalyticsATTListenerDenied()
        // {
        //     GameAnalytics.Initialize();
        // }
        //
        // public void GameAnalyticsATTListenerAuthorized()
        // {
        //     GameAnalytics.Initialize();
        // }


        #endregion

        private void Update()
        {
            if (_isGameStarted) return;
            
            if (Input.GetMouseButtonDown(0))
            {
                if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android)
                {
                    if (Input.touchCount > 0)
                    {
                        if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                        {
                            EventManager.GameStart?.Invoke();
                            _isGameStarted = true;
                        }
                    }
                }
                else
                {
                    if (!EventSystem.current.IsPointerOverGameObject())
                    {
                        EventManager.GameStart?.Invoke();
                        _isGameStarted = true;
                    }
                }
            }
            
        }

        public void RestartLevel()
        {
            var persistData = PersistData.Instance;
            Elephant.LevelFailed(persistData.CurrentLevel);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            persistData.Save();
        }

        public void NextLevel()
        {
            var persistData = PersistData.Instance;
            Elephant.LevelCompleted(persistData.CurrentLevel);

            if (SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCountInBuildSettings - 1)
            {
                persistData.CurrentLevel++;
                SceneManager.LoadScene(Random.Range(persistData.LevelLoopStartIndex, SceneManager.sceneCountInBuildSettings - 1));
            }
            else
            {
                persistData.CurrentLevel++;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            
            persistData.Save();
        }
    }
