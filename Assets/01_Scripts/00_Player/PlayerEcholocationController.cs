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

    private Movement2 PlayerController;
    private float PlayerMovSpeed;
    private bool PlayerIsActive = true;

    private void Awake()
    {
        PlayerController = GetComponent<Movement2>();
    }

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
        PlayerMovSpeed = PlayerController.movSpeed;
        PlayerController.movSpeed = 0;
        yield return ScriptsTools.GetWait(PreReturnTime/2);
        PlayerController.movSpeed = PlayerMovSpeed;
        yield return ScriptsTools.GetWait(PreReturnTime/2);
        ReturnEcholocation();
    }

    void ReturnEcholocation()
    {
        List<IEchoElement> closestElement = PickUpElementsManager.current.GetClosestElement(transform, MaxEchoDistance);

        closestElement.Add(BossEcholocation.current);

        for (int i = 0; i < closestElement.Count; i++)
        {
            closestElement[i].ShowEcholocation();
            
        }
    }
    
    public void AddEcholocationLevel()
    {
        MaxEchoDistance += DistanceIncreaseByLevel;
        PickUpElementsManager.current.AddRangeDistance(DistanceIncreaseByLevel);
    }

    private void ResetEcholocation()
    {
        MaxEchoDistance = StartingMaxEchoDistance;
        PickUpElementsManager.current.ResetRangeDistance(StartingMaxEchoDistance);
    }
}
