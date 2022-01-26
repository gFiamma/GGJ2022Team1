using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseTalk : MonoBehaviour             //video di sfondo: https://www.videezy.com/abstract/40246-deep-space-background-loop
{
    [SerializeField]
    private Dialogue dialogue = default;                                //creo un dialogo tramite il codice
    bool doOnce = false;
    [SerializeField]
    bool wantAResponse;

    public ChooseTalk1 primoDialogo;

    public GameObject dialogueBox;
    private void Start()
    {
        Interact();                                                 //richiamo il metodo interact()
    }

    private void Update()
    {
        if (dialogueBox.activeInHierarchy == false)
        {
            StartCoroutine(changeScene());
        }
    }

    public void Interact()
    {
        if (!doOnce)
        {
            primoDialogo.enabled = true;
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
        }

    }

    IEnumerator changeScene()
    {
        yield return new WaitForSeconds(1f);
        if (DialogueManager.Yes > DialogueManager.No)
        {
            SceneManager.LoadScene("Day1");
        }
        else
        {
            SceneManager.LoadScene("Day1Real");
        }

    }
}
