using System;
using System.Collections;
using System.Collections.Generic;
using ArthemyDevelopment.Localization;
using UnityEngine;

public class WhisperSubController : MonoBehaviour
{
    private Animator WhispersAnim;
    public LocalizationObject whispersText;
    public LocalizationObject whispersTextHindi;

    private Coroutine currHide;
    private void Awake()
    {
        WhispersAnim = GetComponent<Animator>();
    }

    public void SetWhispersSub(float duration, string key)
    {
        if (PlayerPrefs.GetInt("ADLocalizationIndex") != 2)
        {
            whispersText.key = key;
            whispersText.SetLocalizedObject();
        }
        else
        {
            whispersTextHindi.key = key;
            whispersTextHindi.SetLocalizedObject();
        }

        WhispersAnim.Play("ShowWhispers");
        currHide=StartCoroutine(HideSubs(duration));


    }

    IEnumerator HideSubs(float duration)
    {
        yield return ScriptsTools.GetWait(duration);
        WhispersAnim.Play("HideWhispers");
    }

    public void StopWhisper()
    {
        if (currHide == null) return;
        StopCoroutine(currHide);
        WhispersAnim.Play("HideWhispers");
    }
}
