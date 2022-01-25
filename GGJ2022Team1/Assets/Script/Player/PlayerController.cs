
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

    //sprite del player
    [SerializeField]
    SpriteRenderer playerSprite;
    [SerializeField]
    Sprite front, back, sideR, sideL;
    int direction;
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
                direction = 1;
                StartCoroutine(MovePlayer(Vector3.up));
            }else if(Input.GetAxis("Vertical") > 0 && !isMoving)
            {
                direction = 1;
            }
            if (Input.GetAxis("Vertical") < 0 && !isMoving && !PlayerCollision.DownBlocked) //S
            {
                direction = 2;
                StartCoroutine(MovePlayer(Vector3.down));
            }
            else if (Input.GetAxis("Vertical") < 0 && !isMoving)
            {
                direction = 2;
            }
            if (Input.GetAxis("Horizontal") > 0 && !isMoving && !PlayerCollision.RightBlocked) //D
            {
                direction = 3;
                StartCoroutine(MovePlayer(Vector3.right));
            }
            else if (Input.GetAxis("Horizontal") > 0 && !isMoving)
            {
                direction = 3;
            }
            if (Input.GetAxis("Horizontal") < 0 && !isMoving && !PlayerCollision.LeftBlocked) //A
            {
                direction = 4;
                StartCoroutine(MovePlayer(Vector3.left));
            }
            else if (Input.GetAxis("Horizontal") < 0 && !isMoving)
            {
                direction = 4;
            }
        }

        if(direction == 1)
        {
            playerSprite.sprite = back;
        }else if(direction == 2)
        {
            playerSprite.sprite = front;
        }else if(direction == 3)
        {
            playerSprite.sprite = sideR;
        }else if(direction == 4)
        {
            playerSprite.sprite = sideL;
        }
        else
        {
            playerSprite.sprite = front;
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

