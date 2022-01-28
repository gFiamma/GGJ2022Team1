using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject player, teleportTo;
    public Animator animScreen;
    public GameObject bScreen;
    public static bool isRealWorld;
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
        if (!isRealWorld)
        {
            AudioManager.AudioList[0].volume = 0.2f;
            yield return new WaitForSeconds(0.2f);
            AudioManager.AudioList[0].volume = 0.15f;
            yield return new WaitForSeconds(0.2f);
            AudioManager.AudioList[0].volume = 0.1f;
            yield return new WaitForSeconds(0.2f);
            AudioManager.AudioList[0].volume = 0.05f;
            yield return new WaitForSeconds(0.2f);
            AudioManager.AudioList[0].volume = 0f;
        }
        else
        {
            AudioManager.AudioList[1].volume = 0.2f;
            yield return new WaitForSeconds(0.2f);
            AudioManager.AudioList[1].volume = 0.15f;
            yield return new WaitForSeconds(0.2f);
            AudioManager.AudioList[1].volume = 0.1f;
            yield return new WaitForSeconds(0.2f);
            AudioManager.AudioList[1].volume = 0.05f;
            yield return new WaitForSeconds(0.2f);
            AudioManager.AudioList[1].volume = 0f;
        }
        yield return new WaitForSeconds(0.7f);
        player.transform.position = teleportTo.transform.position + new Vector3(1, 0, 0);
        isRealWorld = !isRealWorld;
        yield return new WaitForSeconds(.5f);
        animScreen.SetBool("Transition", false);
        if (!isRealWorld)
        {
            AudioManager.AudioList[1].Stop();
            AudioManager.AudioList[0].Play();

            AudioManager.AudioList[0].volume = 0;
            yield return new WaitForSeconds(0.2f);
            AudioManager.AudioList[0].volume = 0.05f;
            yield return new WaitForSeconds(0.2f);
            AudioManager.AudioList[0].volume = 0.1f;
            yield return new WaitForSeconds(0.2f);
            AudioManager.AudioList[0].volume = 0.15f;
            yield return new WaitForSeconds(0.2f);
            AudioManager.AudioList[0].volume = 0.2f;
        }
        else
        {
            AudioManager.AudioList[0].Stop();
            AudioManager.AudioList[1].Play();

            AudioManager.AudioList[1].volume = 0;
            yield return new WaitForSeconds(0.2f);
            AudioManager.AudioList[1].volume = 0.05f;
            yield return new WaitForSeconds(0.2f);
            AudioManager.AudioList[1].volume = 0.1f;
            yield return new WaitForSeconds(0.2f);
            AudioManager.AudioList[1].volume = 0.15f;
            yield return new WaitForSeconds(0.2f);
            AudioManager.AudioList[1].volume = 0.2f;
        }
        yield return new WaitForSeconds(0.7f);
        PlayerController.canMove = true;
        bScreen.SetActive(false);
        doOnce = false;
    }
}
