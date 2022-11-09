using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    
    public delegate void playerInteract();

    public playerInteract OnPlayerInteract;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.O))
        {
            if(OnPlayerInteract!=null)
                OnPlayerInteract.Invoke();
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PickUpElement"))
        {
            OnPlayerInteract += other.GetComponent<PickUpElementController>().PickUpElement;
        }

        if (other.CompareTag("Boss"))
        {
            OnPlayerInteract += other.GetComponent<BossController>().InteractBoss;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PickUpElement"))
        {
            OnPlayerInteract -= other.GetComponent<PickUpElementController>().PickUpElement;
        }
        if (other.CompareTag("Boss"))
        {
            OnPlayerInteract -= other.GetComponent<BossController>().InteractBoss;
        }
    }
}
