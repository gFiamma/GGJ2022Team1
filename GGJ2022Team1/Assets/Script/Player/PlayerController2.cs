using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2 : MonoBehaviour
{

    //variabili utili per la morte del player
    public static bool isDead;

    //variabili per il movimento
    public static bool canMove;
    public static bool isMoving;
    private Vector3 originPos, targetPos;
    private float timeToMove = 0.35f;
    float movementX;
    float movementY;

    //sprite del player
    [SerializeField]
    SpriteRenderer playerSprite;
    int direction;

    //animator per gestire le animazioni
    public Animator anim;

    void Start()
    {
        canMove = true;
        anim.SetBool("MoveDown", false);
        anim.SetBool("MoveLeft", false);
        anim.SetBool("MoveUp", false);
        anim.SetBool("MoveRight", false);
        anim.SetBool("IdleDown", false);
        anim.SetBool("IdleLeft", false);
        anim.SetBool("IdleUp", false);
        anim.SetBool("IdleRight", false);
    } 

    void Update()
    {
        //controllo per togliere i comandi al player quando è morto e quando è in pausa
        if (!isDead && !Pause.GameIsPaused && !DialogueManager.isTyping && canMove)
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

            if (!isMoving)
            {
                anim.SetBool("MoveDown", false);
                anim.SetBool("MoveLeft", false);
                anim.SetBool("MoveUp", false);
                anim.SetBool("MoveRight", false);
                if(direction == 1)
                {
                    anim.SetBool("IdleUp", true);
                    anim.SetBool("IdleDown", false);
                    anim.SetBool("IdleRight", false);
                    anim.SetBool("IdleLeft", false);
                }
                else if (direction == 2)
                {
                    anim.SetBool("IdleDown", true);
                    anim.SetBool("IdleUp", false);
                    anim.SetBool("IdleRight", false);
                    anim.SetBool("IdleLeft", false);
                }
                else if (direction == 3)
                {
                    anim.SetBool("IdleRight", true);
                    anim.SetBool("IdleUp", false);
                    anim.SetBool("IdleDown", false);
                    anim.SetBool("IdleLeft", false);
                }
                else if (direction == 4)
                {
                    anim.SetBool("IdleLeft", true);
                    anim.SetBool("IdleUp", false);
                    anim.SetBool("IdleDown", false);
                    anim.SetBool("IdleRight", false);
                }

            }

            if (direction == 1 && isMoving)
            {
                anim.SetBool("MoveUp", true);
                anim.SetBool("MoveDown", false);
                anim.SetBool("MoveLeft", false);
                anim.SetBool("MoveRight", false);
                anim.SetBool("IdleUp", false);
            }
            else if (direction == 2 && isMoving)
            {
                anim.SetBool("MoveDown", true);
                anim.SetBool("MoveLeft", false);
                anim.SetBool("MoveUp", false);
                anim.SetBool("MoveRight", false);
                anim.SetBool("IdleDown", false);
            }
            else if (direction == 3 && isMoving)
            {
                anim.SetBool("MoveRight", true);
                anim.SetBool("MoveDown", false);
                anim.SetBool("MoveLeft", false);
                anim.SetBool("MoveUp", false);
                anim.SetBool("IdleRight", false);
            }
            else if (direction == 4 && isMoving)
            {
                anim.SetBool("MoveLeft", true);
                anim.SetBool("MoveDown", false);
                anim.SetBool("MoveUp", false);
                anim.SetBool("MoveRight", false);
                anim.SetBool("IdleLeft", false);
            }
            else
            {
                anim.SetBool("MoveDown", false);
                anim.SetBool("MoveLeft", false);
                anim.SetBool("MoveUp", false);
                anim.SetBool("MoveRight", false);
            }
        }

        if (isDead || !canMove)
        {
            anim.SetBool("MoveDown", false);
            anim.SetBool("MoveLeft", false);
            anim.SetBool("MoveUp", false);
            anim.SetBool("MoveRight", false);
        }
    }
    private IEnumerator MovePlayer(Vector3 direction)
    {
        isMoving = true;

        float elapsedTime = 0;

        originPos = transform.position;
        targetPos = originPos + direction;
        AudioManager.AudioList[16].pitch = Random.Range(1.2f, 1.6f);
        AudioManager.AudioList[16].volume = Random.Range(0.05f, 0.35f);
        AudioManager.AudioList[16].Play();
        while (elapsedTime < timeToMove)
        {
            transform.position = Vector3.Lerp(originPos, targetPos, (elapsedTime / timeToMove));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPos;

        isMoving = false;
    }
}

