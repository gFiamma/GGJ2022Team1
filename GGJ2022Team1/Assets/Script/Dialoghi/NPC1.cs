using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC1 : MonoBehaviour
{
    [SerializeField]
    private Dialogue dialogue = default;                                //creo un dialogo tramite il codice

    [SerializeField]
    private Dialogue dialogue2 = default;                                //creo un dialogo tramite il codice

    public GameObject interactHUD;                                      //prendo in refernce l'HUD
    bool doOnce = false;

    [SerializeField]
    bool wantAResponse;

    private void Update()
    {
        if(DialogueManager.chooseResult != 0)
        {
            doOnce = false;
            Interact();                                                 //richiamo il metodo interact()
        }
    }
    public void Interact()
    {

        if (!doOnce && DialogueManager.chooseResult == 1)
        {
            DialogueManager.chooseResult = 0;
            doOnce = true;
            interactHUD.SetActive(false);                                           //disabilito l'hud
            if (wantAResponse)
            {
                DialogueManager.isChoosing = true;
            }
            else
            {
                DialogueManager.isChoosing = false;
            }
            StartCoroutine(DialogueManager.Instance.ShowDialogue(dialogue));        //faccio partire una coroutine che mostra il dialogo
        }else if(!doOnce && DialogueManager.chooseResult == 2)
        {
            DialogueManager.chooseResult = 0;
            doOnce = true;
            interactHUD.SetActive(false);                                           //disabilito l'hud
            if (wantAResponse)
            {
                DialogueManager.isChoosing = true;
            }
            else
            {
                DialogueManager.isChoosing = false;
            }
            StartCoroutine(DialogueManager.Instance.ShowDialogue(dialogue2));        //faccio partire una coroutine che mostra il dialogo
        }
    }
}
