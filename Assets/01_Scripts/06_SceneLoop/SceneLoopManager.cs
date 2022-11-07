using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SceneLoopManager : SingletonManager<SceneLoopManager>
{
    public UnityEvent OnResetScene;
    public UnityEvent OnFinishScene;

    private void Start()
    {
        ResetScene();
    }


    public void ResetScene()
    {
        OnResetScene.Invoke();
    }

    public void FinishScene()
    {
        OnFinishScene.Invoke();
    }
}
