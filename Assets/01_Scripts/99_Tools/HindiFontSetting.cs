
using UnityEngine;

public class HindiFontSetting : MonoBehaviour
{

    public GameObject Hindi;
    public GameObject Latin;
    public static int HindiIndex= 2;
    private void Awake()
    {
        

            if (HindiFontManager.current.IsHindi)
            {
                
                Hindi.gameObject.SetActive(true);
                Latin.gameObject.SetActive(false);
            }
            else
            {
                Hindi.gameObject.SetActive(false);
                Latin.gameObject.SetActive(true);
                
            }
            
        
    }
}
