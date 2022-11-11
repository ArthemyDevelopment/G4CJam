using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HindiFontManager : SingletonManager<HindiFontManager>
{
    public bool IsHindi;


    private void Awake()
    {
        init();
    }

    public void SetHindi()
    {
        IsHindi = true;
    }
}
