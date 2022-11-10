using System;
using System.Collections;
using System.Collections.Generic;
using ArthemyDevelopment.Localization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(-5)]
public class DialogsManager : SingletonManager<DialogsManager>
{
    
    public delegate void dialogTrigger();

    public dialogTrigger OnDialogFinish;
    
    private Animator DialogAnimation;
    public Image CharacterIcon;
    public LocalizationObject DialogText;
    public bool isPlaying;
    public WhisperController whispers;
    private List<Dialog> currDialogs=new List<Dialog>();
    private Coroutine DialogRutine;
    public float TimePerChar= 0.12f;

    private int currDialogIndex;
    private void Awake()
    {
        init();
        DialogAnimation = GetComponent<Animator>();
    }

    public void SetDialog(Dialog currDialog)
    {
        if (isPlaying) return;
        currDialogs.Add(currDialog);
        GenericSet();
    }

    public void SetDialog(List<Dialog> currDialog)
    {
        if (isPlaying) return;
        currDialogs.AddRange(currDialog);
        GenericSet();
    }

    void GenericSet()
    {
        isPlaying = true;
        whispers.StopWhisper();
        Movement2.current.movSpeed = 0;
        NextDialog();
        DialogAnimation.Play("ShowDialog");
    }
    
    void NextDialog()
    {
        if (currDialogIndex < currDialogs.Count)
        {
            CharacterIcon.sprite = currDialogs[currDialogIndex].DialogCharacter;
            DialogText.key = currDialogs[currDialogIndex].DialogKey;
            DialogText.SetLocalizedObject();
            DialogRutine=StartCoroutine(DialogTimer(DialogText.GetComponent<TMP_Text>().text.Length*TimePerChar));
            currDialogIndex++;
        }
        else
        {
            isPlaying = false;
            DialogAnimation.Play("HideDialog");
            currDialogs.Clear();
            currDialogIndex = 0;
            Movement2.current.movSpeed = Movement2.current.StartMovSpeed;
            if (OnDialogFinish != null)
            {
                OnDialogFinish.Invoke();
                OnDialogFinish = null;
            }
        }
    }
    IEnumerator DialogTimer(float duration)
    {
        yield return ScriptsTools.GetWait(duration);
        NextDialog();
    }

    public void SkipDialog()
    {
        if(!isPlaying)return;
        StopCoroutine(DialogRutine);
        NextDialog();
    }
    
}


[Serializable]
public struct Dialog
{
    public Sprite DialogCharacter;
    public string DialogKey;
    public float DialogDuration;
    //public AudioClip voiceAudio;
}
