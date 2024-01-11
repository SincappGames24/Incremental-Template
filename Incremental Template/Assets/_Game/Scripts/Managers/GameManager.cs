using System.Collections;
using System.Collections.Generic;
using ElephantSDK;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
{
    private bool _isGameStarted;

    private void Awake()
    {
        Input.multiTouchEnabled = false;
        Application.targetFrameRate = 60;
    }

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
                        EventManager.OnGameStart?.Invoke();
                        _isGameStarted = true;
                    }
                }
            }
            else
            {
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    EventManager.OnGameStart?.Invoke();
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
        int loadSceneIndex = 0;
        
        if (SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCountInBuildSettings - 1)
        {
            persistData.CurrentLevel++;
            loadSceneIndex = persistData.LevelLoopStartIndex;
            SceneManager.LoadScene(loadSceneIndex);
        }
        else
        {
            persistData.CurrentLevel++;
            loadSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
            SceneManager.LoadScene(loadSceneIndex);
        }

        persistData.LastSceneIndex = loadSceneIndex;
        persistData.Save();
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            //PersistData.Instance.Save();
        }
    }

    private void OnApplicationQuit()
    {
        //PersistData.Instance.Save();
    }
}