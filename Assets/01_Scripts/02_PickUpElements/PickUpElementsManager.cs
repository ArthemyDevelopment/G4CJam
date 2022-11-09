using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[DefaultExecutionOrder(-5)]
public class PickUpElementsManager : SingletonManager<PickUpElementsManager>
{
    public List<PickUpElementController> PickUpElementsInScene;
    public UnityEvent OnPickUpElement;
    public int PickedAmount=0;

    private void Awake()
    {
        init();
    }
    

    public void ResetElements()
    {
        foreach (var element in PickUpElementsInScene)
        {
            element.ResetElement();
        }

        PickedAmount = 0;
    }


    public void PickUpElement() 
    {
        OnPickUpElement.Invoke();
        PickedAmount++;
    }

    public void AddRangeDistance(float newRange)
    {
        foreach (var element in PickUpElementsInScene)
        {
            element.UpdateEchoRange(newRange);
        }
    }

    public void ResetRangeDistance(float range)
    {
        foreach (var element in PickUpElementsInScene)
        {
            element.ResetEchoRange(range);
        }
    }

    public List<IEchoElement> GetClosestElement(Transform PlayerPos, float MaxDistance)
    {
        List<IEchoElement> tempControllers = new List<IEchoElement>();
        float currentDist = MaxDistance;
        for (int i = 0; i < PickUpElementsInScene.Count; i++)
        {
            float tempDist = Vector3.Distance(PlayerPos.position, PickUpElementsInScene[i].transform.position);
            if (tempDist < currentDist)
            {
                tempControllers.Add(PickUpElementsInScene[i]);
            }
        }

        return tempControllers;
    }
    
    


}
