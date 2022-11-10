using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-5)]
public class EchoDefaultValues : SingletonManager<EchoDefaultValues>
{

    public float MaxDistance;
    public float Speed;
    public float StartFadingPoint;
    private void Awake()
    {
        init();
    }
}
