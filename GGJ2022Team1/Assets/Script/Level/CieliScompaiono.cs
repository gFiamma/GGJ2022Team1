using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CieliScompaiono : MonoBehaviour
{
    public GameObject cieli;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                cieli.SetActive(false);
            }
        }
    }
}
