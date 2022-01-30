using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCEnd : MonoBehaviour
{
    [SerializeField]
    private Dialogue dialogue = default;                                //creo un dialogo tramite il codice
    [SerializeField]
    private bool canInteract;                                           //una booleana per capire se il player può interagire con l'NPC o no
    [SerializeField]
    private GameObject TextInteractPC = default;                        //prendo in reference il testo per PC
    [SerializeField]
    private GameObject TextInteractController = default;                //prendo in reference il testo per controller

    bool doOnce = false;

    private void Start()
    {
        Interact();                                                 //richiamo il metodo interact()
        doOnce = false;
    }

    public void Interact()
    {
        if (!doOnce)
        {
            doOnce = true;
            StartCoroutine(DialogueManager.Instance.ShowDialogue(dialogue));        //faccio partire una coroutine che mostra il dialogo
        }
    }
}
