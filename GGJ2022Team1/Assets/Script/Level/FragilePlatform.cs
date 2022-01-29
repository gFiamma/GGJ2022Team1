using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragilePlatform : MonoBehaviour
{
    public GameObject thisplatform = default;                                                                                       //prendo in reference una piattaforma                                 
    private SpriteRenderer thisSprite = default;                                                                                    //creo una reference privata per lo sprite render della piattaforma
    public Sprite rotto1, rotto2, rotto3, rotto4;
    bool doOnce;
    private void Start()
    {
        doOnce = false;
        thisSprite = thisplatform.gameObject.GetComponent<SpriteRenderer>();
        //prendo lo SpriteRenderer della piattaforma
    }

    private void OnTriggerStay2D(Collider2D other)                                                                                         //se qualcosa entra nel trigger della piattaforma
    {
        if (other.gameObject.CompareTag("Player") && !doOnce)                                   //e quel qualcosa è il player
        {
            doOnce = true;
            StartCoroutine(breakPlatform());                                                                                        //fai partire la coroutine che rompe la piattaforma
        }

    }

    private void OnEnable()
    {
        doOnce = false;
        thisSprite = thisplatform.gameObject.GetComponent<SpriteRenderer>();
        thisSprite.sprite = rotto1;
        thisplatform.gameObject.tag = "Untagged";
    }


    IEnumerator breakPlatform()
    {
        AudioManager.AudioList[11].Play();
        //disabilito lo spriterenderer della piattaforma e poi attendo ripetutamente in modo che sembra che la piattaforma lampeggi
        thisSprite.sprite = rotto2;
        yield return new WaitForSeconds(0.2f);
        thisSprite.enabled = false;
        yield return new WaitForSeconds(0.2f);
        thisSprite.sprite = rotto3;
        thisSprite.enabled = true;
        yield return new WaitForSeconds(0.2f);
        thisSprite.sprite = rotto4;
        thisSprite.enabled = true;
        yield return new WaitForSeconds(0.2f);
        thisSprite.sprite = null;
        thisplatform.gameObject.tag = "death";
        yield return new WaitForSeconds(3f);
        thisplatform.gameObject.tag = "Untagged";
        thisSprite.sprite = rotto1;
        doOnce = false;
    }
}

