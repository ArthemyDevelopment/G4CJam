using System;
using System.Collections;
using System.Collections.Generic;
using ArthemyDevelopment.Localization;
using UnityEngine;
using UnityEngine.UI;

public class DialogsManager : SingletonManager<DialogsManager>
{
    private Animator DialogAnimation;
    public Image CharacterIcon;
    public LocalizationObject DialogText;
    private bool isPlaying;
    
    
    private void Awake()
    {
        init();
        DialogAnimation = GetComponent<Animator>();
    }


    public void SetDialog(Dialog currDialog)
    {
        if (isPlaying) return;
        isPlaying = true;
        CharacterIcon.sprite = currDialog.DialogCharacter;
        DialogText.key = currDialog.DialogKey;
        DialogText.SetLocalizedObject();
        StartCoroutine(StartDialog(currDialog.DialogDuration));
    }

    public void SetDialog(List<Dialog> currDialog)
    {
        
    }
    
    IEnumerator StartDialogs(List<Dialog> currDialog)
    {

        DialogAnimation.Play("ShowDialog");
        for (int i = 0; i < currDialog.Count; i++)
        {
            CharacterIcon.sprite = currDialog[i].DialogCharacter;
            DialogText.key = currDialog[i].DialogKey;
            DialogText.SetLocalizedObject();
            yield return ScriptsTools.GetWait(currDialog[i].DialogDuration);
        }
        DialogAnimation.Play("HideDialog");
        isPlaying = false;
    }

    IEnumerator StartDialog(float duration)
    {
        DialogAnimation.Play("ShowDialog");
        yield return ScriptsTools.GetWait(duration);
        DialogAnimation.Play("HideDialog");
        isPlaying = false;
    }
}

[Serializable]
public struct Dialog
{
    public Sprite DialogCharacter;
    public string DialogKey;
    public float DialogDuration;
}
