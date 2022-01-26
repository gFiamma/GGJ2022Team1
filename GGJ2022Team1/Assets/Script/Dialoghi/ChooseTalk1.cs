using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseTalk1 : MonoBehaviour
{
    [SerializeField]
    private Dialogue dialogue = default;                                //creo un dialogo tramite il codice
    [SerializeField]
    private Dialogue dialogue2 = default;                                //creo un dialogo tramite il codice

    bool doOnce = false;
    [SerializeField]
    bool wantAResponse;
    
    public ChooseTalk1 altroDialogo;

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
            cambiaDialogo();
            DialogueManager.chooseResult = 0;
            doOnce = true;
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
            cambiaDialogo();
            DialogueManager.chooseResult = 0;
            doOnce = true;
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

    void cambiaDialogo()
    {
        altroDialogo.enabled = true;
        this.enabled = false;
    }
}
