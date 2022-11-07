using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerEcholocationController : MonoBehaviour
{
    [FoldoutGroup("Echo Stats")]private float MaxEchoDistance;
    [FoldoutGroup("Echo Stats")]public float PreReturnTime;
    [FoldoutGroup("Echo Stats"), SerializeField] private float StartingMaxEchoDistance;
    [FoldoutGroup("Echo Stats"), SerializeField] private float DistanceIncreaseByLevel;
    [FoldoutGroup("Echo Stats/References")] public EcholocationController PlayerEcho;

    private bool PlayerIsActive = true;
    

    void Update()
    {
        if (!PlayerIsActive) return;
        
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.U))
            StartCoroutine(EcholocationRoutine());
    }

    public void ActivePlayer()
    {
        PlayerIsActive = true;
        ResetEcholocation();
    }

    public void TurnOffPlayer()
    {
        PlayerIsActive = false;
    }
    
    

    IEnumerator EcholocationRoutine()
    {
        PlayerEcho.TriggerEffect(transform.position);
        yield return ScriptsTools.GetWait(PreReturnTime);
        ReturnEcholocation();
    }

    void ReturnEcholocation()
    {
        List<PickUpElementController> closestElement = PickUpElementsManager.current.GetClosestElement(transform, MaxEchoDistance);

        //Add bad creature Echolocation to the list

        for (int i = 0; i < closestElement.Count; i++)
        {
            closestElement[i].ShowEcholocation();
            
        }
    }
    
    public void AddEcholocationLevel()
    {
        MaxEchoDistance += DistanceIncreaseByLevel;
    }

    private void ResetEcholocation()
    {
        MaxEchoDistance = StartingMaxEchoDistance;
    }
}
