using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogChangePlayer : MonoBehaviour
{
    public string SceneName;

    public Animator anim;
    public GameObject blackScreen;

    [SerializeField]
    private NPCEnd npcEnd;
    [SerializeField]
    private GameObject dialogueBox;
    bool doOnce = false;

    [SerializeField]
    private bool canInteract;                                           //una booleana per capire se il player può interagire con l'NPC o no
    [SerializeField]
    private GameObject TextInteractPC = default;                        //prendo in reference il testo per PC
    [SerializeField]
    private GameObject TextInteractController = default;                //prendo in reference il testo per controller
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !canInteract)
        {

            if (!dialogueBox.activeInHierarchy && !doOnce)
            {
                doOnce = true;
                StartCoroutine(waitEnd());
            }
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump") && canInteract && DialogueManager.isTyping == false)          //se premi il tasto per interagire, puoi interagire, l'NPC non sta parlando e non hai già interagito
        {
            canInteract = false;
            npcEnd.enabled = true;                                        //richiamo lo script del player
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (Teleport.isRealWorld)
            {
                DialogueManager.No += 10;
                Debug.Log(DialogueManager.No);
            }
            else
            {
                DialogueManager.Yes += 10;
                Debug.Log(DialogueManager.Yes);
            }

            if (SceneName == "Vittoria")
            {
                if (DialogueManager.Yes > DialogueManager.No)
                {
                    SceneName = "Vittoria1";
                }
                else if (DialogueManager.Yes < DialogueManager.No)
                {
                    SceneName = "Vittoria2";
                }
                else
                {
                    SceneName = "Vittoria3";
                }
            }

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
            npcEnd.enabled = false;
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

    IEnumerator waitEnd()
    {
        blackScreen.SetActive(true);
        anim.SetBool("Transition", true);
        PlayerController.canMove = false;
        PlayerController2.canMove = false;
        yield return new WaitForSeconds(1.2f);
        anim.SetBool("Transition", false);
        SceneManager.LoadScene(SceneName);
    }
}
