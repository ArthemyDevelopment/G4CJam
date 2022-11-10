using System;
using System.Collections;
using System.Collections.Generic;
using ArthemyDevelopment.Localization;
using UnityEngine;

public class WhisperSubController : MonoBehaviour
{
    private Animator WhispersAnim;
    public LocalizationObject whispersText;

    private Coroutine currHide;
    private void Awake()
    {
        WhispersAnim = GetComponent<Animator>();
    }

    public void SetWhispersSub(float duration, string key)
    {
        whispersText.key = key;
        whispersText.SetLocalizedObject();
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
