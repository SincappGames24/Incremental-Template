using System;
using System.Collections;
using System.Collections.Generic;
using SincappStudio;
using UnityEngine;

public class LevelManager : MonoSingleton<LevelManager>
{
    public PlayerController PlayerController { get; private set; }
    
    protected override void Awake()
    {
      base.Awake();
      PlayerController = FindObjectOfType<PlayerController>();
    }
}
