using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySiringa : MonoBehaviour
{
    //variabili per il movimento
    bool doOnce;
    private Vector3 originPos, targetPos;
    private float timeToMove = 0.35f;
    [SerializeField]
    private float movSpeed = default;
    int direction;
    float x = 0;
    float y = 0;
    bool isMoving;

    [SerializeField]
    private SpriteRenderer enemy;
    [SerializeField]
    private Sprite mov1, mov2, mov3, mov4;

    [SerializeField]
    private EnemyCollision enemyCollision;

    [SerializeField] AudioSource suonoPassi;
    void Start()
    {
        isMoving = false;
        doOnce = false;
    }

    void Update()
    {
        if (!doOnce)
        {
            doOnce = true;
            calcolaDirezione();
            Vector3 asd = new Vector3(x, y, 0);
            if(asd.x == 0 || asd.y == 0)
            {
                if (asd.x == 0 && asd.y == 1)
                {
                    enemy.sprite = mov1;
                }
                else if (asd.x == 0 && asd.y == -1)
                {
                    enemy.sprite = mov2;
                }
                else if (asd.x == 1 && asd.y == 0)
                {
                    enemy.sprite = mov3;
                }
                else if (asd.x == -1 && asd.y == 0)
                {
                    enemy.sprite = mov4;
                }

                StartCoroutine(MovePlayer(asd));
            }

        }
    }

    private IEnumerator MovePlayer(Vector3 direction)
    {
        float elapsedTime = 0;

        originPos = transform.position;
        targetPos = originPos + direction;
        if (isMoving)
        {
            suonoPassi.pitch = Random.Range(1.2f, 1.6f);
            suonoPassi.volume = Random.Range(0.05f, 0.35f);
            suonoPassi.Play();
        }

        while (elapsedTime < timeToMove)
        {
            transform.position = Vector3.Lerp(originPos, targetPos, (elapsedTime / timeToMove));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPos;
        yield return new WaitForSeconds(movSpeed);
        doOnce = false;
    }

    void calcolaDirezione()
    {
        //4
        if (this.enemyCollision.UpBlocked && this.enemyCollision.DownBlocked &&
        this.enemyCollision.LeftBlocked && this.enemyCollision.RightBlocked)
        {
            isMoving = false;
            x = 0;
            y = 0;
        }
        //3
        else if (this.enemyCollision.UpBlocked && this.enemyCollision.DownBlocked &&
            this.enemyCollision.LeftBlocked)
        {
            isMoving = true;
            x = 1;
            y = 0;
        }
        else if (this.enemyCollision.UpBlocked && this.enemyCollision.LeftBlocked &&
        this.enemyCollision.RightBlocked)
        {
            isMoving = true;
            x = 0;
            y = -1;
        }
        else if (this.enemyCollision.DownBlocked && this.enemyCollision.RightBlocked &&
        this.enemyCollision.LeftBlocked)
        {
            isMoving = true;
            x = 0;
            y = 1;
        }
        else if (this.enemyCollision.UpBlocked && this.enemyCollision.DownBlocked &&
        this.enemyCollision.RightBlocked)
        {
            isMoving = true;
            x = -1;
            y = 0;
        }
        //2
        else if (this.enemyCollision.UpBlocked && this.enemyCollision.DownBlocked)
        {
            x = (int)Random.Range(-2f, 2f);
            y = 0;
            if (x == 0)
            {
                isMoving = false;
            }
            else
            {
                isMoving = true;
            }
        }
        else if (this.enemyCollision.UpBlocked && this.enemyCollision.RightBlocked)
        {
            isMoving = true;
            x = (int)Random.Range(-2f, 1f);
            y = (int)Random.Range(-2f, 1f);

            if (x != 0)
            {
                y = 0;
            }
            else if (y != 0)
            {
                x = 0;
            }
            if (x == 0 && y == 0)
            {
                isMoving = false;
            }
            else
            {
                isMoving = true;
            }
        }
        else if (this.enemyCollision.UpBlocked && this.enemyCollision.LeftBlocked)
        {
            isMoving = true;
            x = (int)Random.Range(-1f, 2f);
            y = (int)Random.Range(-2f, 1f);

            if (x != 0)
            {
                y = 0;
            }
            else if (y != 0)
            {
                x = 0;
            }
            if (x == 0 && y == 0)
            {
                isMoving = false;
            }
            else
            {
                isMoving = true;
            }
        }
        else if (this.enemyCollision.DownBlocked && this.enemyCollision.RightBlocked)
        {
            isMoving = true;
            x = (int)Random.Range(-2f, 1f);
            y = (int)Random.Range(-1f, 2f);

            if (x != 0)
            {
                y = 0;
            }
            else if (y != 0)
            {
                x = 0;
            }
            if (x == 0 && y == 0)
            {
                isMoving = false;
            }
            else
            {
                isMoving = true;
            }
        }
        else if (this.enemyCollision.DownBlocked && this.enemyCollision.LeftBlocked)
        {
            isMoving = true;
            x = (int)Random.Range(-1f, 2f);
            y = (int)Random.Range(-1f, 2f);

            if (x != 0)
            {
                y = 0;
            }
            else if (y != 0)
            {
                x = 0;
            }
            if (x == 0 && y == 0)
            {
                isMoving = false;
            }
            else
            {
                isMoving = true;
            }
        }
        else if (this.enemyCollision.LeftBlocked && this.enemyCollision.RightBlocked)
        {
            isMoving = true;
            x = 0;
            y = (int)Random.Range(-2f, 2f);

            if (x != 0)
            {
                y = 0;
            }
            else if (y != 0)
            {
                x = 0;
            }
            if (x == 0 && y == 0)
            {
                isMoving = false;
            }
            else
            {
                isMoving = true;
            }
        }
        //1
        else if (this.enemyCollision.UpBlocked)
        {
            isMoving = true;
            x = (int)Random.Range(-2f, 2f);
            y = (int)Random.Range(-2f, 1f);

            if (x != 0)
            {
                y = 0;
            }
            else if (y != 0)
            {
                x = 0;
            }
            if (x == 0 && y == 0)
            {
                isMoving = false;
            }
            else
            {
                isMoving = true;
            }
        }
        else if (this.enemyCollision.DownBlocked)
        {
            isMoving = true;
            x = (int)Random.Range(-2f, 2f);
            y = (int)Random.Range(-1f, 2f);

            if (x != 0)
            {
                y = 0;
            }
            else if (y != 0)
            {
                x = 0;
            }
            if (x == 0 && y == 0)
            {
                isMoving = false;
            }
            else
            {
                isMoving = true;
            }

        }
        else if (this.enemyCollision.LeftBlocked)
        {
            isMoving = true;
            x = (int)Random.Range(-1f, 2f);
            y = (int)Random.Range(-2f, 2f);

            if (x != 0)
            {
                y = 0;
            }
            else if (y != 0)
            {
                x = 0;
            }
            if (x == 0 && y == 0)
            {
                isMoving = false;
            }
            else
            {
                isMoving = true;
            }
        }
        else if (this.enemyCollision.RightBlocked)
        {
            isMoving = true;
            x = (int)Random.Range(-2f, 1f);
            y = (int)Random.Range(-2f, 2f);

            if (x != 0)
            {
                y = 0;
            }
            else if (y != 0)
            {
                x = 0;
            }
            if (x == 0 && y == 0)
            {
                isMoving = false;
            }
            else
            {
                isMoving = true;
            }
        }
        else if (!this.enemyCollision.UpBlocked && !this.enemyCollision.DownBlocked &&
        !this.enemyCollision.LeftBlocked && !this.enemyCollision.RightBlocked)
        {
            isMoving = true;
            x = (int)Random.Range(-2f, 2f);
            y = (int)Random.Range(-2f, 2f);

            if (x != 0)
            {
                y = 0;
            }
            else if (y != 0)
            {
                x = 0;
            }
            if (x == 0 && y == 0)
            {
                isMoving = false;
            }
            else
            {
                isMoving = true;
            }
        }

    }
}

