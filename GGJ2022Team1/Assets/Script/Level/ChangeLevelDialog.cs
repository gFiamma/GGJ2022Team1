using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLevelDialog : MonoBehaviour
{
    public string SceneName;
    public Animator anim;
    public GameObject blackScreen;

    [SerializeField]
    private NPCEnd npcEnd;
    [SerializeField]
    private GameObject dialogueBox;

    bool doOnce = default;

    private void Start()
    {
        doOnce = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!dialogueBox.activeInHierarchy && !doOnce)
            {
                doOnce = true;
                StartCoroutine(waitEnd());
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(SceneName == "Vittoria")
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
            npcEnd.enabled = true;
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
