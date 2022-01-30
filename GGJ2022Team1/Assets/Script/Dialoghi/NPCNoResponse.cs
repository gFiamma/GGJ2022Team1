using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCNoResponse : MonoBehaviour
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

    private void Update()
    {

        if (Input.GetButtonDown("Jump") && canInteract && DialogueManager.isTyping == false)          //se premi il tasto per interagire, puoi interagire, l'NPC non sta parlando e non hai già interagito
        {
            Interact();                                                 //richiamo il metodo interact()
        }
    }

    private void OnTriggerEnter2D(Collider2D other)                         //se qualcosa entra nel trigger
    {
        if (other.gameObject.CompareTag("Player") && DialogueManager.isTyping == false)         //e quel qualcosa è il player e l'NPC non sta parlando
        {
            doOnce = false;
            canInteract = true;                                         //canInteract viene settata a true
            if (!doOnce)
            {
                if (ControllerCheck.controllerPlugged == true)              //se il controller è collegato
                {
                    TextInteractController.SetActive(true);                 //attivo il testo per controller
                    TextInteractPC.SetActive(false);                        //disattivo il testo per pc
                }
                else                                                        //sennò
                {
                    TextInteractController.SetActive(false);                //disattivo il testo per controller
                    TextInteractPC.SetActive(true);                         //attivo il testo per pc
                }
            }

        }
    }

    private void OnTriggerExit2D(Collider2D other)                          //se qualcosa esce dal trigger
    {
        if (other.gameObject.CompareTag("Player") && !DialogueManager.isTyping)                      //e quel qualcos è il player
        {
            canInteract = false;
            if (ControllerCheck.controllerPlugged == true)              //se il controller è collegato
            {
                TextInteractController.SetActive(false);                //disattivo il testo per controller
                TextInteractPC.SetActive(false);                        //disattivo il testo per pc
            }
            else
            {
                TextInteractPC.SetActive(false);                        //disattivo il testo per pc
                TextInteractController.SetActive(false);                //disattivo il testo per controller
            }
        }
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
