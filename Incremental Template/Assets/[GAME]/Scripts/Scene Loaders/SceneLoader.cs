using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private void Awake()
    {
        var persistData = PersistData.Instance;
        var targetLevelIndex = persistData.CurrentLevel + 1;
        
        if (targetLevelIndex > SceneManager.sceneCountInBuildSettings - 1)
        {
            SceneManager.LoadScene((persistData.CurrentLevel - 1) % (SceneManager.sceneCountInBuildSettings - 2) + persistData.LevelLoopStartIndex);
        }
        else
        {
            SceneManager.LoadScene(targetLevelIndex);
        }
    }
}