using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    public GameObject fullEnemy;
    public bool RightBlocked, LeftBlocked, UpBlocked, DownBlocked;
    public GameObject right, left, up, down;
    public float distance = 0.3f;
    public LayerMask ObstacleMask;

    public bool isMuffin;

    private void Update()
    {
        RightBlocked = Physics2D.OverlapCircle(right.transform.position, distance, ObstacleMask);
        LeftBlocked = Physics2D.OverlapCircle(left.transform.position, distance, ObstacleMask);
        UpBlocked = Physics2D.OverlapCircle(up.transform.position, distance, ObstacleMask);
        DownBlocked = Physics2D.OverlapCircle(down.transform.position, distance, ObstacleMask);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(right.transform.position, distance);
        Gizmos.DrawWireSphere(left.transform.position, distance);
        Gizmos.DrawWireSphere(up.transform.position, distance);
        Gizmos.DrawWireSphere(down.transform.position, distance);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Up") || collision.gameObject.CompareTag("Down") || 
            collision.gameObject.CompareTag("Left") || collision.gameObject.CompareTag("Right"))
        {
            if (isMuffin)
            {
                AudioManager.AudioList[13].Play();
            }
            else
            {
                AudioManager.AudioList[14].Play();
            }

            Destroy(fullEnemy);
        }
    }
}
