using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportSameWorld : MonoBehaviour
{
    public GameObject player, teleportTo;
    public Animator animScreen;
    public GameObject bScreen;
    bool doOnce;

    [SerializeField]
    bool left = default;
    [SerializeField]
    bool right = default;
    [SerializeField]
    bool up = default;
    [SerializeField]
    bool down = default;
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
        PlayerController2.canMove = false;
        bScreen.SetActive(true);
        animScreen.SetBool("Transition", true);
        yield return new WaitForSeconds(1.3f);
        AudioManager.AudioList[12].Play();

        if(left && !right && !up && !down)
        {
            player.transform.position = teleportTo.transform.position + new Vector3(1, 0, 0);
        }
        if (right && !left && !up && !down)
        {
            player.transform.position = teleportTo.transform.position + new Vector3(-1, 0, 0);
        }
        if(up && !left && !down && !right)
        {
            player.transform.position = teleportTo.transform.position + new Vector3(0, -1, 0);
        }
        if(down && !left && !right && !up)
        {
            player.transform.position = teleportTo.transform.position + new Vector3(0, 1, 0);
        }

        yield return new WaitForSeconds(.5f);
        animScreen.SetBool("Transition", false);
        yield return new WaitForSeconds(1.3f);
        PlayerController.canMove = true;
        PlayerController2.canMove = true;
        bScreen.SetActive(false);
        doOnce = false;
    }
}
