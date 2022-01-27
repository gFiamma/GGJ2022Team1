using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject player, teleportTo;
    public Animator animScreen;
    public GameObject bScreen;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(Teleporting());
    }

    IEnumerator Teleporting()
    {
        PlayerController.canMove = false;
        bScreen.SetActive(true);
        animScreen.SetBool("Transition", true);
        yield return new WaitForSeconds(1.5f);
        player.transform.position = teleportTo.transform.position + new Vector3(1, 0, 0);
        yield return new WaitForSeconds(.5f);
        animScreen.SetBool("Transition", false);
        yield return new WaitForSeconds(1.5f);
        PlayerController.canMove = true;
        bScreen.SetActive(false);
    }
}
