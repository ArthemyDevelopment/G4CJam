using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionBatsController : MonoBehaviour
{
    public List<GameObject> Bats;
    private int currBat;

    private void Awake()
    {
        ResetBats();
    }

    public void ShowBat(int i)
    {
        Bats[i].SetActive(true);
    }

    public void ResetBats()
    {
        foreach (var bat in Bats)
        {
            bat.SetActive(false);
        }
    }
}
