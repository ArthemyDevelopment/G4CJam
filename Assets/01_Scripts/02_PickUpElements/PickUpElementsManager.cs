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
    }


    public void PickUpElement() 
    {
        OnPickUpElement.Invoke();
    }

    public List<PickUpElementController> GetClosestElement(Transform PlayerPos, float MaxDistance)
    {
        List<PickUpElementController> tempControllers = new List<PickUpElementController>();
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
