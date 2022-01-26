
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //variabili utili per la morte del player
    public static bool isDead;

    //variabili per il movimento
    public static bool isMoving;
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

    public Animator anim;
    bool isAttacking;
    void Start()
    {
        anim.SetBool("MoveDown", false);
        anim.SetBool("MoveLeft", false);
        anim.SetBool("MoveUp", false);
        anim.SetBool("MoveRight", false);
        isAttacking = false;
    } 

    void Update()
    {
        //controllo per togliere i comandi al player quando è morto e quando è in pausa
        if (!isDead && !Pause.GameIsPaused && !DialogueManager.isTyping)
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

            if(!isMoving && !isAttacking)
            {
                anim.SetBool("MoveDown", false);
                anim.SetBool("MoveLeft", false);
                anim.SetBool("MoveUp", false);
                anim.SetBool("MoveRight", false);
            }
        }

        if (direction == 1 && isMoving && !isAttacking)
        {
            anim.SetBool("MoveUp", true);
            anim.SetBool("MoveDown", false);
            anim.SetBool("MoveLeft", false);
            anim.SetBool("MoveRight", false);
            //playerSprite.sprite = back;
        }
        else if (direction == 2 && isMoving && !isAttacking)
        {
            anim.SetBool("MoveDown", true);
            anim.SetBool("MoveLeft", false);
            anim.SetBool("MoveUp", false);
            anim.SetBool("MoveRight", false);
            //playerSprite.sprite = front;
        }
        else if (direction == 3 && isMoving && !isAttacking)
        {
            anim.SetBool("MoveRight", true);
            anim.SetBool("MoveDown", false);
            anim.SetBool("MoveLeft", false);
            anim.SetBool("MoveUp", false);
            //playerSprite.sprite = sideR;
        }
        else if (direction == 4 && isMoving && !isAttacking)
        {
            anim.SetBool("MoveLeft", true);
            anim.SetBool("MoveDown", false);
            anim.SetBool("MoveUp", false);
            anim.SetBool("MoveRight", false);
            //playerSprite.sprite = sideL;
        }
        else
        {
            anim.SetBool("MoveDown", false);
            anim.SetBool("MoveLeft", false);
            anim.SetBool("MoveUp", false);
            anim.SetBool("MoveRight", false);
            //playerSprite.sprite = front;
        }


        if (Input.GetButtonDown("Back") && direction == 1 && !isAttacking)
        {
            isAttacking = true;
            anim.SetBool("AtkUp", true);
            StartCoroutine(atkWait());
        }
        else if (Input.GetButtonDown("Back") && direction == 2 && !isAttacking)
        {
            isAttacking = true;
            anim.SetBool("AtkDown", true);
            StartCoroutine(atkWait());
        }
        else if (Input.GetButtonDown("Back") && direction == 3 && !isAttacking)
        {
            isAttacking = true;
            anim.SetBool("AtkRight", true);
            StartCoroutine(atkWait());
        }
        else if (Input.GetButtonDown("Back") && direction == 4 && !isAttacking)
        {
            isAttacking = true;
            anim.SetBool("AtkLeft", true);
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

    private IEnumerator atkWait()
    {
        yield return new WaitForSeconds(1.2f);
        anim.SetBool("MoveDown", false);
        anim.SetBool("MoveLeft", false);
        anim.SetBool("MoveUp", false);
        anim.SetBool("MoveRight", false);
        isAttacking = false;
    }
}

