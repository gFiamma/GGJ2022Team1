using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure; //Serve per la vibrazione del controller

public class PlayerController : MonoBehaviour
{
    //servono per la vibrazione del controller
    PlayerIndex playerIndex;
    GamePadState state;
    GamePadState prevState;

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

    //variabili per l'attacco
    public GameObject leftBox, rightBox, UpBox, DownBox;
    bool isAttacking;
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
        isAttacking = false;
        leftBox.GetComponent<BoxCollider2D>().enabled = false;
        rightBox.GetComponent<BoxCollider2D>().enabled = false;
        UpBox.GetComponent<BoxCollider2D>().enabled = false;
        DownBox.GetComponent<BoxCollider2D>().enabled = false;
    } 

    void Update()
    {
        //controllo per togliere i comandi al player quando è morto e quando è in pausa
        if (!isDead && !Pause.GameIsPaused && !DialogueManager.isTyping && canMove)
        {
            if (Input.GetAxis("Vertical") > 0 && !isMoving && !PlayerCollision.UpBlocked && !isAttacking) //W
            {
                direction = 1;
                StartCoroutine(MovePlayer(Vector3.up));
            }else if(Input.GetAxis("Vertical") > 0 && !isMoving && !isAttacking)
            {
                direction = 1;
            }
            if (Input.GetAxis("Vertical") < 0 && !isMoving && !PlayerCollision.DownBlocked && !isAttacking) //S
            {
                direction = 2;
                StartCoroutine(MovePlayer(Vector3.down));
            }
            else if (Input.GetAxis("Vertical") < 0 && !isMoving)
            {
                direction = 2;
            }
            if (Input.GetAxis("Horizontal") > 0 && !isMoving && !PlayerCollision.RightBlocked && !isAttacking) //D
            {
                direction = 3;
                StartCoroutine(MovePlayer(Vector3.right));
            }
            else if (Input.GetAxis("Horizontal") > 0 && !isMoving)
            {
                direction = 3;
            }
            if (Input.GetAxis("Horizontal") < 0 && !isMoving && !PlayerCollision.LeftBlocked && !isAttacking) //A
            {
                direction = 4;
                StartCoroutine(MovePlayer(Vector3.left));
            }
            else if (Input.GetAxis("Horizontal") < 0 && !isMoving && !isAttacking)
            {
                direction = 4;
            }

            if (!isMoving && !isAttacking)
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

            if (direction == 1 && isMoving && !isAttacking)
            {
                anim.SetBool("MoveUp", true);
                anim.SetBool("MoveDown", false);
                anim.SetBool("MoveLeft", false);
                anim.SetBool("MoveRight", false);
                anim.SetBool("IdleUp", false);
            }
            else if (direction == 2 && isMoving && !isAttacking)
            {
                anim.SetBool("MoveDown", true);
                anim.SetBool("MoveLeft", false);
                anim.SetBool("MoveUp", false);
                anim.SetBool("MoveRight", false);
                anim.SetBool("IdleDown", false);
            }
            else if (direction == 3 && isMoving && !isAttacking)
            {
                anim.SetBool("MoveRight", true);
                anim.SetBool("MoveDown", false);
                anim.SetBool("MoveLeft", false);
                anim.SetBool("MoveUp", false);
                anim.SetBool("IdleRight", false);
            }
            else if (direction == 4 && isMoving && !isAttacking)
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

            if (Input.GetButtonDown("Back") && direction == 1 && !isAttacking)
            {
                isAttacking = true;
                Vibrate();
                UpBox.GetComponent<BoxCollider2D>().enabled = true;
                anim.SetBool("AtkUp", true);
                AudioManager.AudioList[15].Play();
                StartCoroutine(atkWait());
            }
            else if (Input.GetButtonDown("Back") && direction == 2 && !isAttacking)
            {
                isAttacking = true;
                Vibrate();
                DownBox.GetComponent<BoxCollider2D>().enabled = true;
                anim.SetBool("AtkDown", true);
                AudioManager.AudioList[15].Play();
                StartCoroutine(atkWait());
            }
            else if (Input.GetButtonDown("Back") && direction == 3 && !isAttacking)
            {
                isAttacking = true;
                Vibrate();
                rightBox.GetComponent<BoxCollider2D>().enabled = true;
                anim.SetBool("AtkRight", true);
                AudioManager.AudioList[15].Play();
                StartCoroutine(atkWait());
            }
            else if (Input.GetButtonDown("Back") && direction == 4 && !isAttacking)
            {
                isAttacking = true;
                Vibrate();
                leftBox.GetComponent<BoxCollider2D>().enabled = true;
                anim.SetBool("AtkLeft", true);
                AudioManager.AudioList[15].Play();
                StartCoroutine(atkWait());
            }

            if (!isAttacking)
            {
                anim.SetBool("AtkUp", false);
                anim.SetBool("AtkDown", false);
                anim.SetBool("AtkRight", false);
                anim.SetBool("AtkLeft", false);
            }
        }

        if (isDead || !canMove)
        {
            anim.SetBool("MoveDown", false);
            anim.SetBool("MoveLeft", false);
            anim.SetBool("MoveUp", false);
            anim.SetBool("MoveRight", false);
            anim.SetBool("AtkUp", false);
            anim.SetBool("AtkDown", false);
            anim.SetBool("AtkRight", false);
            anim.SetBool("AtkLeft", false);
        }
    }
    private IEnumerator MovePlayer(Vector3 direction)
    {
        isMoving = true;

        float elapsedTime = 0;

        originPos = transform.position;
        targetPos = originPos + direction;
        AudioManager.AudioList[8].pitch = Random.Range(1.2f, 1.6f);
        AudioManager.AudioList[8].volume = Random.Range(0.05f, 0.35f);
        AudioManager.AudioList[8].Play();
        while (elapsedTime < timeToMove)
        {
            transform.position = Vector3.Lerp(originPos, targetPos, (elapsedTime / timeToMove));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPos;

        isMoving = false;
    }

    private IEnumerator atkWait()
    {
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("MoveDown", false);
        anim.SetBool("MoveLeft", false);
        anim.SetBool("MoveUp", false);
        anim.SetBool("MoveRight", false);
        isAttacking = false;
        leftBox.GetComponent<BoxCollider2D>().enabled = false;
        rightBox.GetComponent<BoxCollider2D>().enabled = false;
        UpBox.GetComponent<BoxCollider2D>().enabled = false;
        DownBox.GetComponent<BoxCollider2D>().enabled = false;
    }

    void Vibrate()
    {
        GamePad.SetVibration(playerIndex, .3f, .3f);                 //IL CONTROLLER VIBRA
        StartCoroutine("StopVibrate");
    }
    IEnumerator StopVibrate()
    {
        yield return new WaitForSeconds(0.3f);
        GamePad.SetVibration(playerIndex, 0f, 0f);                  //IL CONTROLLER SMETTE DI VIBRARE
    }
}

