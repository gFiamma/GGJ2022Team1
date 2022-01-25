
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //variabili utili per la morte del player
    public static bool isDead;

    //variabili per il movimento
    public bool isMoving;
    private Vector3 originPos, targetPos;
    private float timeToMove = 0.35f;
    float movementX;
    float movementY;

    void Start()
    {

    } 

    void Update()
    {
        //controllo per togliere i comandi al player quando è morto e quando è in pausa
        if (!isDead && !Pause.GameIsPaused && !DialogueManager.isTyping)
        {
            if (Input.GetAxis("Vertical") > 0 && !isMoving && !PlayerCollision.UpBlocked) //W
            {
                StartCoroutine(MovePlayer(Vector3.up));
            }
            if (Input.GetAxis("Vertical") < 0 && !isMoving && !PlayerCollision.DownBlocked) //S
            {
                StartCoroutine(MovePlayer(Vector3.down));
            }
            if (Input.GetAxis("Horizontal") > 0 && !isMoving && !PlayerCollision.RightBlocked) //D
            {
                StartCoroutine(MovePlayer(Vector3.right));
            }
            if (Input.GetAxis("Horizontal") < 0 && !isMoving && !PlayerCollision.LeftBlocked) //A
            {
                StartCoroutine(MovePlayer(Vector3.left));
            }
        }
    }
    private IEnumerator MovePlayer(Vector3 direction)
    {
        isMoving = true;

        float elapsedTime = 0;

        originPos = transform.position;
        targetPos = originPos + direction;

        while(elapsedTime < timeToMove)
        {
            transform.position = Vector3.Lerp(originPos, targetPos, (elapsedTime / timeToMove));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPos;

        isMoving = false;
    }
}

