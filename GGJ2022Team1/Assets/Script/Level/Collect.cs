using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collect : MonoBehaviour
{
    public float rotationSpeed = 100.0f;

    private void Update()
    {
        transform.Rotate(new Vector3(0,0,rotationSpeed * Time.deltaTime));
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            AudioManager.AudioList[4].Play();
            Inventory.collezionabile++;
            Destroy(gameObject);

        }
    }
}
