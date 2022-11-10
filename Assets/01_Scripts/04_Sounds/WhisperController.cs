using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WhisperController : MonoBehaviour
{
    public List<AudioSource> WhispersSources;
    public List<Whispers> Whispers;
    public WhisperSubController WhispersSubs;
    public Vector2 RandomRange;
    bool isPlaying;
    bool whisperActive;
    private int source;
    private int whisper;
    private void OnEnable()
    {
        isPlaying = false;
    }
    
    

    public void StartWhispers()
    {
        isPlaying = true;
        StartCoroutine(WhisperLoop());
    }

    public void TurnOff()
    {
        isPlaying = false;
    }

    IEnumerator WhisperLoop()
    {
        while (isPlaying)
        {
            float temp = Random.Range(RandomRange.x, RandomRange.y);
            yield return ScriptsTools.GetWait(temp);
            if (!whisperActive)
            {
                whisperActive = true;
                source = Random.Range(0, WhispersSources.Count);
                whisper = Random.Range(0, Whispers.Count);
                PlayWhisper(source, whisper);
                yield return ScriptsTools.GetWait(Whispers[whisper].WhisperClip.length);
                whisperActive = false;
            }

        }
    }

    void PlayWhisper(int source, int whisper)
    {
        if (DialogsManager.current.isPlaying) return;
        WhispersSources[source].clip = Whispers[whisper].WhisperClip;
        WhispersSources[source].Play();
        WhispersSubs.SetWhispersSub(Whispers[whisper].WhisperClip.length, Whispers[whisper].LocalizationKey);
    }

    public void StopWhisper()
    {
        WhispersSources[source].Stop();
        WhispersSubs.StopWhisper();
    }
    
}

[Serializable]
public struct Whispers
{
    public AudioClip WhisperClip;
    public string LocalizationKey;
}
