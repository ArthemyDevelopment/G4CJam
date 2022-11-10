using System.Collections.Generic;
using UnityEngine;

public class IntroDialog : MonoBehaviour
{
   public List<Dialog> introDialog;

   private void OnEnable()
   {
      DialogsManager.current.SetDialog(introDialog);
   }
}
