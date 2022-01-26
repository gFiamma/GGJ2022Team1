using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    //riferimento al particellare da far partire quando viene preso il checkpoint
    public ParticleSystem particles;

    //private AudioManager AudioManag;

    /// Indicate if the checkpoint is activated
    public bool Activated = false;                                                                                         //setto la bool dell'attivazione del checkpoint a falso

    /// List with all checkpoints objects in the scene
    public static GameObject[] CheckPointsList;                                                                             //setto una variavile static per controllare la lista dei vari checkpoint

    /// Get position of the last activated checkpoint

    public Sprite checkActive, checkDisabled;


    public static Vector3 GetActiveCheckPointPosition()                                                                     //cerchiamo la posizione dell'ultimo checkpoint attivo
    {
        // If player die without activate any checkpoint, we will return a default position
        Vector3 result = new Vector3(0f, 0f, 0f);                                                                              //setto la posizione base del respawn

        if (CheckPointsList != null)
        {
            foreach (GameObject cp in CheckPointsList)
            {
                // We search the activated checkpoint to get its position
                if (cp.GetComponent<Checkpoint>().Activated)                                                                   //viene cercato il checkpoint attivo
                {
                    result = cp.transform.position;                                                                            //viene cambiata la posizione di transform al respawn
                    break;
                }
            }
        }

        return result;
    }

    /// Activate the checkpoint

    private void ActivateCheckPoint()
    {
        // We deactive all checkpoints in the scene
        foreach (GameObject cp in CheckPointsList)
        {
            //cp.GetComponent<SpriteRenderer>().color = Color.white;                                                          //resettiamo il colore a tutti i checkpoint
            cp.GetComponent<SpriteRenderer>().sprite = checkDisabled;                                                          //resettiamo lo sprite renderer a tutti i checkpoint
            cp.GetComponent<Checkpoint>().Activated = false;                                                                //disattiviamo tutti i checkpoint
        }

        // We activated the current checkpoint
        Activated = true;                                                                                                   //attiviamo solo il checkpoint corrente
        //GetComponent<SpriteRenderer>().color = Color.green;                                                              //cambiamo il colore al checkpoint attivo
        GetComponent<SpriteRenderer>().sprite = checkActive;                                                          //attiviamo lo sprite renderer al checkpoint attivo
    }

    void Start()
    {
        //AudioManag = FindObjectOfType<AudioManager>();

        // We search all the checkpoints in the current scene
        CheckPointsList = GameObject.FindGameObjectsWithTag("CheckPoint");                                                  //vengono cercati tutti i checkpoint nella scena
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // If the player passes through the checkpoint, we activate it
        if (other.gameObject.CompareTag("Player") && !this.Activated)                                                                                          //se il player collide con il checkpoint
        {
            ActivateCheckPoint();                                                                                           //il checkpoint viene attivato
            particles.Play();                                                                                                   //fa partire il particellare
            //audio Checkpoint
        }
    }
}



