using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLevel : MonoBehaviour
{
    public string SceneName;
    public Animator anim;
    public GameObject blackScreen;

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
            StartCoroutine(waitEnd());
        }
    }

    IEnumerator waitEnd()
    {
        blackScreen.SetActive(true);
        anim.SetBool("Transition", true);
        PlayerController.canMove = false;
        PlayerController2.canMove = false;
        yield return new WaitForSeconds(1.2f);
        SceneManager.LoadScene(SceneName);
    }
}
