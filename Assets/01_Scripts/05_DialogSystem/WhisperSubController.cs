using System;
using System.Collections;
using System.Collections.Generic;
using ArthemyDevelopment.Localization;
using UnityEngine;

public class WhisperSubController : MonoBehaviour
{
    private Animator WhispersAnim;
    public LocalizationObject whispersText;

    private void Awake()
    {
        WhispersAnim = GetComponent<Animator>();
    }

    public void SetWhispersSub(float duration, string key)
    {
        whispersText.key = key;
        whispersText.SetLocalizedObject();
        WhispersAnim.Play("ShowWhispers");
        StartCoroutine(HideSubs(duration));


    }

    IEnumerator HideSubs(float duration)
    {
        yield return ScriptsTools.GetWait(duration);
        WhispersAnim.Play("HideWhispers");
    }
}
