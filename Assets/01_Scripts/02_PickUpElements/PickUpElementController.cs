using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PickUpElementController : MonoBehaviour, IEchoElement
{
    private bool alreadyPickedUp = false;
    public UnityEvent LocalOnPickElement;
    public EcholocationController Echolocation;
    public List<Dialog> dialog;

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
        TriggerDialog();
        if (alreadyPickedUp) return;
        alreadyPickedUp = true;
        DialogsManager.current.OnDialogFinish+=LocalOnPickElement.Invoke;
        DialogsManager.current.OnDialogFinish+=PickUpElementsManager.current.PickUpElement;
    }

    public void ResetElement()
    {
        alreadyPickedUp = false;
        
    }

    public void UpdateEchoRange(float newRange)
    {
        Echolocation.UpdateEchoRange(newRange);
    }

    public void ResetEchoRange(float range)
    {
        Echolocation.ResetEchoRange(range);   
    }

    public void TriggerDialog()
    {
        DialogsManager.current.SetDialog(dialog);
    }

    public void ShowEcholocation()
    {
        if (alreadyPickedUp) return;
        Echolocation.TriggerEffect(transform.position);
    }
}

public interface IEchoElement
{
    public void ShowEcholocation();
}
