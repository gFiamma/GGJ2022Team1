using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPlayer : MonoBehaviour
{
    public PlayerCollision playerCollision;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerCollision.Death();
        }
    }
}
