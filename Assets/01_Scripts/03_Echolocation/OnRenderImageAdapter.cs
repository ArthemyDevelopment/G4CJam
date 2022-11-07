using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnRenderImageAdapter : MonoBehaviour
{
    private EcholocationEffect[] AllEchoEffects;


    private void OnEnable()
    {
        AllEchoEffects = FindObjectsOfType<EcholocationEffect>(true);
        Debug.Log(AllEchoEffects);
    }

    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        RenderTexture currDest=null;
        for (int i = 0; i < AllEchoEffects.Length; i++)
        {
            if(i==0)
                AllEchoEffects[i].OnRenderImage(src,dest);
            else
            {
                AllEchoEffects[i].OnRenderImage(currDest,dest);

            }

            currDest = dest;
        }
        
    }


}
