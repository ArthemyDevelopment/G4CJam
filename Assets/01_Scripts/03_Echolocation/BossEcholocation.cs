using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-5)]
public class BossEcholocation : SingletonManager<BossEcholocation>, IEchoElement
{
    private EcholocationController controller;
    private void Awake()
    {
        init();
        controller = GetComponent<EcholocationController>();
    }

    public void ShowEcholocation()
    {
        controller.TriggerEffect(transform.position);
    }
}
