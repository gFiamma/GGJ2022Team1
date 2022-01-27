using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //variabili per il movimento
    bool doOnce;
    bool isMoving;
    private Vector3 originPos, targetPos;
    private float timeToMove = 0.35f;
    [SerializeField]
    private float movSpeed = default;

    int direction;
    void Start()
    {
        doOnce = false;
    }

    void Update()
    {
        if (!doOnce)
        {
            doOnce = true;
            float x = (int)Random.Range(-2f, 2f);
            float y = (int)Random.Range(-2f, 2f);
            Debug.Log(x);
            Debug.Log(y);

            if (EnemyCollision.DownBlocked) y = (int)Random.Range(-1f, 2f);
            if (EnemyCollision.UpBlocked) y = (int)Random.Range(-2f, 1f);
            if (EnemyCollision.LeftBlocked) x = (int)Random.Range(-1f, 2f);
            if (EnemyCollision.RightBlocked) x = (int)Random.Range(-2f, 1f);

            if (x != 0)
            {
                y = 0;
            }
            else if (y != 0)
            {
                x = 0;
            }
            else
            {
                x = 0;
                y = 0;
            }

            Vector3 asd = new Vector3(x, y, 0);
            StartCoroutine(MovePlayer(asd));
        }
    }

    private IEnumerator MovePlayer(Vector3 direction)
    {
        isMoving = true;

        float elapsedTime = 0;

        originPos = transform.position;
        targetPos = originPos + direction;

        while (elapsedTime < timeToMove)
        {
            transform.position = Vector3.Lerp(originPos, targetPos, (elapsedTime / timeToMove));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPos;
        yield return new WaitForSeconds(movSpeed);
        isMoving = false;
        doOnce = false;
    }
}

