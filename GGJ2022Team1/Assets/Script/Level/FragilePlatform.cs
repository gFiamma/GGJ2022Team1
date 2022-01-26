using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragilePlatform : MonoBehaviour
{
    public GameObject thisplatform = default;                                                                                       //prendo in reference una piattaforma                                 
    private SpriteRenderer thisSprite = default;                                                                                    //creo una reference privata per lo sprite render della piattaforma
    public Sprite rotto1, rotto2;
    bool doOnce;
    //private AudioManager AudioManag;
    private void Start()
    {
        doOnce = false;
        thisSprite = thisplatform.gameObject.GetComponent<SpriteRenderer>();
        //prendo lo SpriteRenderer della piattaforma

        //AudioManag = FindObjectOfType<AudioManager>();
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
        //thisplatform.GetComponent<BoxCollider2D>().enabled = true;
        thisplatform.gameObject.tag = "Untagged";
    }


    IEnumerator breakPlatform()
    {
        //AudioManag.AudioBello[14].Play();
        //disabilito lo spriterenderer della piattaforma e poi attendo ripetutamente in modo che sembra che la piattaforma lampeggi
        thisSprite.sprite = rotto2;
        yield return new WaitForSeconds(0.2f);
        thisSprite.enabled = false;
        yield return new WaitForSeconds(0.1f);
        thisSprite.enabled = true;
        yield return new WaitForSeconds(0.1f);
        thisSprite.enabled = true;
        yield return new WaitForSeconds(0.3f);
        thisSprite.sprite = null;
        //thisplatform.GetComponent<BoxCollider2D>().enabled = false;
        thisplatform.gameObject.tag = "death";
        //invece di disattivare il box collider, bisogna metterne uno che uccide il player
        yield return new WaitForSeconds(1.5f);
        thisplatform.gameObject.tag = "Untagged";
        //thisplatform.GetComponent<BoxCollider2D>().enabled = true;
        thisSprite.sprite = rotto1;
        doOnce = false;
    }
}

