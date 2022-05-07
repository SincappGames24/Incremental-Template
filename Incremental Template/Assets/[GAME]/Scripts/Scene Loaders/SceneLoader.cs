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
        
        if (persistData.CurrentLevel > 11)
        {
            SceneManager.LoadScene(UnityEngine.Random.Range(4, 11));
        }
        else
        {
            SceneManager.LoadScene(persistData.CurrentLevel);
        }
    }
}