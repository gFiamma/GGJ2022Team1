using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject player, teleportTo;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(Teleporting());
    }

    IEnumerator Teleporting()
    {
        player.GetComponent<PlayerController>().enabled = false;
        yield return new WaitForSeconds(.5f);
        player.transform.position = teleportTo.transform.position + new Vector3(1, 0, 0);
        yield return new WaitForSeconds(.5f);
        player.GetComponent<PlayerController>().enabled = true;
    }
}
