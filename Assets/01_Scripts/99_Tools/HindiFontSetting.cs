
using UnityEngine;

public class HindiFontSetting : MonoBehaviour
{

    public GameObject Hindi;
    public GameObject Latin;
    public static int HindiIndex= 2;
    private void Awake()
    {
        if (PlayerPrefs.HasKey("ADLocalizationIndex"))
        {
            int temp= PlayerPrefs.GetInt("ADLocalizationIndex");

            if (temp == HindiIndex)
            {
                
                Hindi.gameObject.SetActive(true);
                Latin.gameObject.SetActive(false);
            }
            else
            {
                Hindi.gameObject.SetActive(true);
                Latin.gameObject.SetActive(false);
                
            }
            
        }
    }
}
