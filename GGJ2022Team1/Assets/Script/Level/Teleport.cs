using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject player, teleportTo;
    public Animator animScreen;
    public GameObject bScreen;
    public static bool isRealWorld;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            StartCoroutine(Teleporting());
        }
    }

    IEnumerator Teleporting()
    {
        PlayerController.canMove = false;
        bScreen.SetActive(true);
        animScreen.SetBool("Transition", true);
        if (!isRealWorld)
        {
            AudioManager.AudioList[0].volume = 0.4f;
            yield return new WaitForSeconds(0.1f);
            AudioManager.AudioList[0].volume = 0.35f;
            yield return new WaitForSeconds(0.1f);
            AudioManager.AudioList[0].volume = 0.3f;
            yield return new WaitForSeconds(0.1f);
            AudioManager.AudioList[0].volume = 0.25f;
            yield return new WaitForSeconds(0.1f);
            AudioManager.AudioList[0].volume = 0.2f;
            yield return new WaitForSeconds(0.1f);
            AudioManager.AudioList[0].volume = 0.15f;
            yield return new WaitForSeconds(0.1f);
            AudioManager.AudioList[0].volume = 0.1f;
            yield return new WaitForSeconds(0.1f);
            AudioManager.AudioList[0].volume = 0.05f;
            yield return new WaitForSeconds(0.1f);
            AudioManager.AudioList[0].volume = 0f;
        }
        else
        {
            AudioManager.AudioList[1].volume = 0.4f;
            yield return new WaitForSeconds(0.1f);
            AudioManager.AudioList[1].volume = 0.35f;
            yield return new WaitForSeconds(0.1f);
            AudioManager.AudioList[1].volume = 0.3f;
            yield return new WaitForSeconds(0.1f);
            AudioManager.AudioList[1].volume = 0.25f;
            yield return new WaitForSeconds(0.1f);
            AudioManager.AudioList[1].volume = 0.2f;
            yield return new WaitForSeconds(0.1f);
            AudioManager.AudioList[1].volume = 0.15f;
            yield return new WaitForSeconds(0.1f);
            AudioManager.AudioList[1].volume = 0.1f;
            yield return new WaitForSeconds(0.1f);
            AudioManager.AudioList[1].volume = 0.05f;
            yield return new WaitForSeconds(0.1f);
            AudioManager.AudioList[1].volume = 0f;
        }
        yield return new WaitForSeconds(0.7f);
        player.transform.position = teleportTo.transform.position + new Vector3(1, 0, 0);
        yield return new WaitForSeconds(.5f);
        animScreen.SetBool("Transition", false);
        if (isRealWorld)
        {
            AudioManager.AudioList[1].Stop();
            AudioManager.AudioList[0].Play();

            AudioManager.AudioList[0].volume = 0;
            yield return new WaitForSeconds(0.1f);
            AudioManager.AudioList[0].volume = 0.05f;
            yield return new WaitForSeconds(0.1f);
            AudioManager.AudioList[0].volume = 0.1f;
            yield return new WaitForSeconds(0.1f);
            AudioManager.AudioList[0].volume = 0.15f;
            yield return new WaitForSeconds(0.1f);
            AudioManager.AudioList[0].volume = 0.2f;
            yield return new WaitForSeconds(0.1f);
            AudioManager.AudioList[0].volume = 0.25f;
            yield return new WaitForSeconds(0.1f);
            AudioManager.AudioList[0].volume = 0.3f;
            yield return new WaitForSeconds(0.1f);
            AudioManager.AudioList[0].volume = 0.35f;
            yield return new WaitForSeconds(0.1f);
            AudioManager.AudioList[0].volume = 0.4f;
        }
        else
        {
            AudioManager.AudioList[0].Stop();
            AudioManager.AudioList[1].Play();

            AudioManager.AudioList[1].volume = 0;
            yield return new WaitForSeconds(0.1f);
            AudioManager.AudioList[1].volume = 0.05f;
            yield return new WaitForSeconds(0.1f);
            AudioManager.AudioList[1].volume = 0.1f;
            yield return new WaitForSeconds(0.1f);
            AudioManager.AudioList[1].volume = 0.15f;
            yield return new WaitForSeconds(0.1f);
            AudioManager.AudioList[1].volume = 0.2f;
            yield return new WaitForSeconds(0.1f);
            AudioManager.AudioList[1].volume = 0.25f;
            yield return new WaitForSeconds(0.1f);
            AudioManager.AudioList[1].volume = 0.3f;
            yield return new WaitForSeconds(0.1f);
            AudioManager.AudioList[1].volume = 0.35f;
            yield return new WaitForSeconds(0.1f);
            AudioManager.AudioList[1].volume = 0.4f;
        }
        yield return new WaitForSeconds(0.7f);
        PlayerController.canMove = true;
        bScreen.SetActive(false);
        isRealWorld = !isRealWorld;
    }
}
