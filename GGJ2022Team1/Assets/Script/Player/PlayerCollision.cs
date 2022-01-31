using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerCollision : MonoBehaviour
{
    public static bool RightBlocked, LeftBlocked, UpBlocked, DownBlocked;
    public GameObject right, left, up, down;
    public float distance = 0.3f;
    public LayerMask ObstacleMask;
    public BoxCollider2D col;
    public GameObject player, bScreen;
    public Animator animScreen, animHud, playerAnim;
    bool doOnce;

    private void Start()
    {
        doOnce = false;
    }
    private void Update()
    {
        RightBlocked = Physics2D.OverlapCircle(right.transform.position, distance, ObstacleMask);
        LeftBlocked = Physics2D.OverlapCircle(left.transform.position, distance, ObstacleMask);
        UpBlocked = Physics2D.OverlapCircle(up.transform.position, distance, ObstacleMask);
        DownBlocked = Physics2D.OverlapCircle(down.transform.position, distance, ObstacleMask);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("death") && !doOnce)
        {
            Death();
        }
    }

    public void Death()
    {
        StartCoroutine(DeathCoroutine());
    }

    IEnumerator DeathCoroutine()
    {
        doOnce = true;
        col.enabled = false;
        PlayerController.isDead = true;
        playerAnim.SetTrigger("Dead");
        Inventory.vite--;
        AudioManager.AudioList[7].Play();
        yield return new WaitForSeconds(1f);
        bScreen.SetActive(true);
        animScreen.SetBool("Transition", true);
        yield return new WaitForSeconds(2f);
        if (Inventory.vite == 0)
        {
            SceneManager.LoadScene("GameOver");
        }
        player.transform.position = Checkpoint.GetActiveCheckPointPosition();
        yield return new WaitForSeconds(1f);
        animScreen.SetBool("Transition", false);
        yield return new WaitForSeconds(1.2f);
        bScreen.SetActive(false);
        col.enabled = true;
        PlayerController.isDead = false;
        animHud.SetTrigger("active");

        doOnce = false;
    }
}
