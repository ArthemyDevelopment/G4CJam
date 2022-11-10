using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EcholocationController : MonoBehaviour
{

    public List<EcholocationEffect> EcholocationEffects;
    public float TimeBetweenEffects;


    public void TriggerEffect(Vector3 pos)
    {
        StartCoroutine(EcholocationRoutine(pos));
    }

    IEnumerator EcholocationRoutine(Vector3 pos)
    {
        for (int i = 0; i < EcholocationEffects.Count; i++)
        {
            EcholocationEffects[i].TriggerEcholocation(pos);
            yield return ScriptsTools.GetWait(TimeBetweenEffects);
        }
    }
    public void UpdateEchoRange(float newRange)
    {
        foreach (var echo in EcholocationEffects)
        {
            echo.MaxDistance += newRange;
            echo.StartFadingPoint = echo.MaxDistance * 0.8f;
        }
    }

    public void ResetEchoRange(float range)
    {
        foreach (var echo in EcholocationEffects)
        {
            echo.MaxDistance = range;
            echo.StartFadingPoint = echo.MaxDistance * 0.8f;
        }
    }
    
    
    
}
