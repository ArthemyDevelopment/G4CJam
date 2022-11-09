using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossController : MonoBehaviour
{
    private bool alreadyInteract;
    public List<Dialog> BossDialogsWin;
    public List<Dialog> BossDialogsLose;
    public Animator EndgameCanvas;
    public float EndGameTime;


    public void InteractBoss()
    {
        if (alreadyInteract) return;
        alreadyInteract = true;
        if (PickUpElementsManager.current.PickedAmount == 5)
        {
            DialogsManager.current.SetDialog(BossDialogsWin);
            DialogsManager.current.OnDialogFinish += FinishGame;
        }
        else
        {
            DialogsManager.current.SetDialog(BossDialogsLose);
            DialogsManager.current.OnDialogFinish += ResetGame;
            
        }
    }

    void ResetGame()
    {
        StartCoroutine(Reset());
        alreadyInteract = false;
    }

    IEnumerator Reset()
    {
        SceneLoopManager.current.FinishScene();
        EndgameCanvas.Play("Reset");
        yield return ScriptsTools.GetWait(EndGameTime);
        SceneLoopManager.current.ResetScene();
        
    }

    void FinishGame()
    {
        StartCoroutine(Finish());
    }
    
    IEnumerator Finish()
    {
        SceneLoopManager.current.FinishScene();
        SceneLoopManager.current.FinishScene();
        EndgameCanvas.Play("Finish");
        yield return ScriptsTools.GetWait(EndGameTime);
        SceneManager.LoadScene((int)SceneIndex.Credits);
    }
}
