using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportSameWorld : MonoBehaviour
{
    public GameObject player, teleportTo;
    public Animator animScreen;
    public GameObject bScreen;
    bool doOnce;

    private void Start()
    {
        doOnce = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !doOnce)
        {
            StartCoroutine(Teleporting());
        }
    }

    IEnumerator Teleporting()
    {
        doOnce = true;
        PlayerController.canMove = false;
        bScreen.SetActive(true);
        animScreen.SetBool("Transition", true);
        yield return new WaitForSeconds(1.3f);
        AudioManager.AudioList[12].Play();
        player.transform.position = teleportTo.transform.position + new Vector3(0, -1, 0);
        yield return new WaitForSeconds(.5f);
        animScreen.SetBool("Transition", false);
        yield return new WaitForSeconds(1.3f);
        PlayerController.canMove = true;
        bScreen.SetActive(false);
        doOnce = false;
    }
}
