using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementDialog : MonoBehaviour
{
    public Dialog dialog;
    
    public void TriggerDialog()
    {
        DialogsManager.current.SetDialog(dialog);
    }
    
}
