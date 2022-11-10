

using TMPro;
using UnityEngine;

public class HindiFontSetting : MonoBehaviour
{

    public static TMP_FontAsset HindiFont;
    public static TMP_FontAsset LatinFont;
    public static int HindiIndex;
    private void OnEnable()
    {
        if (PlayerPrefs.HasKey("ADLocalizationIndex"))
        {
            int temp= PlayerPrefs.GetInt("ADLocalizationIndex");

            if (temp == HindiIndex)
            {
                GetComponent<TMP_Text>().font = HindiFont;
            }
            else
            {
                GetComponent<TMP_Text>().font = LatinFont;
                
            }
            
        }
    }
}
