using UnityEngine;

public class IntroDialog : MonoBehaviour
{
   public Dialog introDialog;

   private void OnEnable()
   {
      DialogsManager.current.SetDialog(introDialog);
   }
}
