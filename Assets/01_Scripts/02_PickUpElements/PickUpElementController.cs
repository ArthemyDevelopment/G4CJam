using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PickUpElementController : MonoBehaviour
{
    private bool alreadyPickedUp = false;
    public UnityEvent LocalOnPickElement;
    public EcholocationController Echolocation;


    private void OnEnable()
    {
        if(!PickUpElementsManager.current.PickUpElementsInScene.Contains(this))
            PickUpElementsManager.current.PickUpElementsInScene.Add(this);
    }

    private void OnDisable()
    {
        if(PickUpElementsManager.current.PickUpElementsInScene.Contains(this))
            PickUpElementsManager.current.PickUpElementsInScene.Remove(this);
    }

    public void PickUpElement()
    {
        if (alreadyPickedUp) return;
        alreadyPickedUp = true;
        LocalOnPickElement.Invoke();
        PickUpElementsManager.current.PickUpElement();
    }

    public void ResetElement()
    {
        alreadyPickedUp = false;
        
    }

    public void ShowEcholocation()
    {
        Echolocation.TriggerEffect(transform.position);
    }
}
