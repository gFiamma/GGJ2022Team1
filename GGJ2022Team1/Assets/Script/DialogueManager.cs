using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{                                                                       //script del dialogo basato su questo https://www.youtube.com/watch?v=2CmG7ZtrWso adattato e modificato secondo le mie esigenze
    [SerializeField]
    private GameObject dialogueBox = default;                           //prendo in reference il Pannello che conterrà il testo
    [SerializeField]
    private Text dialogueText = default;                                //prendo in reference il testo che permetterà di visualizzare il dialogo
    [SerializeField]
    private int lettersPerSecond = default;                             //creo una variabile che determina la velocità con il quale le lettere vengono visualizzate
    [SerializeField]
    private int lettersPerSecondStarter = default;                      //creo una variabile che tiene la velocità con il quale le lettere vengono visualizzate
    [SerializeField]
    private GameObject TextInteractPC = default;                        //prendo in reference il testo per pc
    [SerializeField]
    private GameObject TextInteractController = default;                //prendo in reference il testo per controller

    [SerializeField]
    private GameObject HUD = default;                                   //prendo in reference l'intero pannello dell'hud

    public AudioSource suonoDialog = default;                           //prendo in reference un suono che verrà riprodotto quando l'NPC parlerà

    bool hoMessoPausa;                                                  //una bool per capire quando si è andati in pausa

    bool nextLine;                                                      //creo una variabile simile che controlla il testo che indica il tasto da premere per andare avanti nel dialogo
    public GameObject NextLineText;                                    //creo una variabile simile a TextInteract per gestire il testo che indica il tasto da premere per andare avanti nel dialogo
    public GameObject NextLineTextController;                           //^
    public static DialogueManager Instance { get; private set; }        //creo un instanza del dialoguemanager

    bool backPressed;

    private void Awake()                                                //prima ancora dello start
    {
        Instance = this;                                                //setto l'istanza a questo script
        isTyping = false;                                               
        nextLine = false;
        lettersPerSecondStarter = lettersPerSecond;
    }
    Dialogue dial;                                                      //creo una variabile di tipo Dialogue
    int currentLine = 0;                                                //creo un indice per capire quale string leggere
    public static bool isTyping;                                        //creo una variabile booleana statica che fa capire quando il player sta parlando con l'NPC

    private void Update()
    {

        if (suonoDialog.isPlaying && Pause.GameIsPaused)                //se stai riproducendo il suono dell'NPC che parla ed il gioco viene messo in pausa
        {
            hoMessoPausa = true;                                        //la variabile che controlla in questo script se il gioco è stato messo in pausa viene settata a true
            suonoDialog.Pause();                                        //il suono viene messo in pausa
        }

        if (isTyping && nextLine && Input.GetButtonDown("Jump"))
        {
            ++currentLine;                                                                              //aumento l'indice per poter far dire la prossima riga all'npc
            if (currentLine < dial.Lines.Count)                                                         //se l'indice non supera il numero di righe
            {
                StartCoroutine(TypeDialogue(dial.Lines[currentLine]));                                  //ripeti la coroutine, ma stavolta con l'indice spostato in modo da leggere la stringa successiva
            }
            else                                                                                        //sennò
            {
                suonoDialog.Stop();                                                                     //il suono di quando parla l'NPC si ferma
                currentLine = 0;                                                                        //resetto l'indice del dialogo
                dialogueBox.SetActive(false);                                                           //disabilito il pannello dove si mostra il dialogo
                HUD.SetActive(true);                                                                    //riattivo l'HUD
                StartCoroutine(waitJump());                                                             //dato che dava problemi che saltava appena finiva il dialogo, ho deciso di fare così per evitare problemi
            }
        }

        if (Input.GetButtonDown("Back"))                                        //se viene premuto il tasto "Back" rendo true la booleana, sennò è false
        {
            backPressed = true;

        }
        if (Input.GetButtonUp("Back"))
        {
            backPressed = false;
        }

    }
    public IEnumerator ShowDialogue(Dialogue dial)                      //coroutine che mostra il dialogo
    {
        yield return new WaitForEndOfFrame();                           //attendi che finisce l'ultimo frame

        this.dial = dial;                                               //il dialogo di questo script è uguale a dialogo passato
        dialogueBox.SetActive(true);                                    //attivo il dialogueBox
        StartCoroutine(TypeDialogue(dial.Lines[0]));                    //faccio partire una coroutine che fa visualizzare il dialogo poco a poco con effetto macchina da scrivere, partendo dall'indice 0 dell'array con le varie strings
    }

    public IEnumerator TypeDialogue(string dialog)                      //coroutine che fa l'effetto macchina da scrivere
    {
        nextLine = false;
        if (DialogueManager.isTyping == true)                            //se il player sta parlando con un NPC
        {
            if (ControllerCheck.controllerPlugged == true)              //se il controller è collegato
            {
                NextLineText.gameObject.SetActive(false);                     //attiva il testo per pc
                NextLineTextController.gameObject.SetActive(false);              //attiva il testo per Controller
            }
            else                                                        //sennò
            {
                NextLineText.gameObject.SetActive(false);                      //attiva il testo per pc
                NextLineTextController.gameObject.SetActive(false);             //attiva il testo per Controller
            }
        }
        else                                                           //sennò
        {
            NextLineText.gameObject.SetActive(false);                         //attiva il testo per pc
            NextLineTextController.gameObject.SetActive(false);                 //attiva il testo per Controller
        }
        suonoDialog.Play();                                             //faccio partire il suono di quando l'NPC parla
        HUD.SetActive(false);                                           //disattivo l'HUD
        isTyping = true;                                                //metto la variabile a true così ora tutti gli altri script sanno che sto interagendo con l'NPC

        if (ControllerCheck.controllerPlugged == true)                  //se il controller è collegato
        {
            TextInteractPC.SetActive(false);                            //disabilito il testo per pc
            TextInteractController.SetActive(false);                    //disabilito il testo per controller
        }
        else                                                            //sennò
        {
            TextInteractPC.SetActive(false);                            //disabilito il testo per pc
            TextInteractController.SetActive(false);                    //disabilito il testo per Controller
        }

        dialogueText.text = "";                                         //setto il testo a nulla

        foreach (var letter in dialog.ToCharArray())                        //per ogni lettera contenuta nella string
        {
            if (!suonoDialog.isPlaying && Pause.GameIsPaused == false && hoMessoPausa == true)      //se il suono non sta venendo riprodotto, il gioco non è messo in pausa, e homessoinpausa è messo a true
            {
                hoMessoPausa = false;                                                               //ho messo in pausa lo setto a false
                suonoDialog.Play();                                                                 //faccio partire il suono del dialogo
            }
            dialogueText.text += letter;                                                            //aggiungo al testo le lettere una ad una

            if (backPressed)                                                    //in questo modo, faccio fare tutto in mezzo secondo per far apparire tutto il testo premendo un tasto senza aspettare
            {
                dialogueText.text = "";                                         //setto il testo a nulla
                foreach (var letter2 in dialog.ToCharArray())                        //per ogni lettera contenuta nella string
                {
                    if (!suonoDialog.isPlaying && Pause.GameIsPaused == false && hoMessoPausa == true)      //se il suono non sta venendo riprodotto, il gioco non è messo in pausa, e homessoinpausa è messo a true
                    {
                        hoMessoPausa = false;                                                               //ho messo in pausa lo setto a false
                        suonoDialog.Play();                                                                 //faccio partire il suono del dialogo
                    }
                    dialogueText.text += letter2;                                                   
                }
                break;
            }
            yield return new WaitForSeconds(1f / lettersPerSecond);                                 //attendo 1 secondo fratto le lettere per secondi
        }
        suonoDialog.Stop();                                                                         //ferma il suono del dialogo appena finisce di leggere la riga
        yield return new WaitForSeconds(0.3f);                                                      //attendo 0.7 secondi
        nextLine = true;
        if (DialogueManager.isTyping == true)                            //se il player sta parlando con un NPC
        {
            if (ControllerCheck.controllerPlugged == true)              //se il controller è collegato
            {
                NextLineText.gameObject.SetActive(false);                     //attiva il testo per pc
                NextLineTextController.gameObject.SetActive(true);              //attiva il testo per Controller
            }
            else                                                        //sennò
            {
                NextLineText.gameObject.SetActive(true);                      //attiva il testo per pc
                NextLineTextController.gameObject.SetActive(false);             //attiva il testo per Controller
            }
        }
        else                                                           //sennò
        {
            NextLineText.gameObject.SetActive(false);                         //attiva il testo per pc
            NextLineTextController.gameObject.SetActive(false);                 //attiva il testo per Controller
        }
    }

    IEnumerator waitJump()
    {
        yield return new WaitForSeconds (0.1f);
        isTyping = false;                                                   //ridà al player il potere di muoversi
    }
}
