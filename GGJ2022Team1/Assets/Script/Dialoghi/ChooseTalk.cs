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
    [SerializeField]
    AudioSource musica;
    [SerializeField]
    Animator anim;
    [SerializeField]
    GameObject blackScreen;

    public ChooseTalk1 primoDialogo;

    public GameObject dialogueBox;
    private void Start()
    {
        StartCoroutine(animScreen());
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
        SceneManager.LoadScene("Day1");
        musica.volume = 0.4f;
        yield return new WaitForSeconds(0.1f);
        musica.volume = 0.35f;
        yield return new WaitForSeconds(0.1f);
        musica.volume = 0.3f;
        yield return new WaitForSeconds(0.1f);
        musica.volume = 0.25f;
        yield return new WaitForSeconds(0.1f);
        musica.volume = 0.2f;
        yield return new WaitForSeconds(0.1f);
        musica.volume = 0.15f;
        yield return new WaitForSeconds(0.1f);
        musica.volume = 0.1f;
        yield return new WaitForSeconds(0.1f);
        musica.volume = 0.05f;
        yield return new WaitForSeconds(0.1f);
        musica.volume = 0f;
    }

    IEnumerator animScreen()
    {
        blackScreen.SetActive(true);
        anim.SetBool("Transition", true);
        yield return new WaitForSeconds(1.2f);
        anim.SetBool("Transition", false);
        yield return new WaitForSeconds(1.2f);
        blackScreen.SetActive(false);
    }
}
