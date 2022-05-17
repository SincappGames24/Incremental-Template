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
        
        if (persistData.CurrentLevel + 1 > SceneManager.sceneCountInBuildSettings - 1)
        {
            SceneManager.LoadScene(UnityEngine.Random.Range(persistData.LevelLoopStartIndex, SceneManager.sceneCountInBuildSettings - 1));
        }
        else
        {
            SceneManager.LoadScene(persistData.CurrentLevel + 1);
        }
    }
}