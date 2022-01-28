using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeCollect : MonoBehaviour
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
            AudioManager.AudioList[5].Play();
            Inventory.vite++;
            Destroy(gameObject);
        }
    }
}
