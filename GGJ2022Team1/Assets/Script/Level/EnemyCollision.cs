using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    public static bool RightBlocked, LeftBlocked, UpBlocked, DownBlocked;
    public GameObject right, left, up, down;
    public float distance = 0.5f;
    public LayerMask ObstacleMask;

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
}
