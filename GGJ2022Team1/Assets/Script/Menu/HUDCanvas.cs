using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HUDCanvas : MonoBehaviour
{
    [SerializeField]
    private Text life, collect;
    [SerializeField]
    private int viteT, collezionabileT;

    public Animator animHud;
    bool doOnce = false;
    bool doOnce2 = false;

    // Update is called once per frame
    private void Start()
    {
        viteT = Inventory.vite;
        collezionabileT = Inventory.collezionabile;
    }

    void Update()
    {
        life.text = "x" + viteT;
        collect.text = "" + collezionabileT;

        if (Input.GetKeyDown(KeyCode.A) && animHud.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !animHud.IsInTransition(0) && !PlayerController.isDead && !Pause.GameIsPaused && !DialogueManager.isTyping && PlayerController.canMove)
        {
            animHud.SetTrigger("active");
        }

        if(viteT < Inventory.vite)
        {
            calcolaVite(viteT);
        }
        else if (PlayerController.isDead)
        {
            viteT = Inventory.vite;
        }


        if (collezionabileT < Inventory.collezionabile)
        {
            calcolaCollect(collezionabileT);
        }
        else if (PlayerController.isDead)
        {
            collezionabileT = Inventory.collezionabile;
        }
    }

    void calcolaVite(int vitaP)
    {
        vitaP = Inventory.vite - vitaP;
        while (vitaP > 0 && !doOnce)
        {
            doOnce = true;
            vitaP--;
            StartCoroutine(animVite());
        }
    }

    IEnumerator animVite()
    {
        if(animHud.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !animHud.IsInTransition(0))
        {
            animHud.SetTrigger("active");
        }
        yield return new WaitForSeconds(1f);
        viteT++;
        yield return new WaitForSeconds(0.5f);
        doOnce = false;
    }

    void calcolaCollect(int collectP)
    {
        collectP = Inventory.collezionabile - collectP;
        while (collectP > 0 && !doOnce2)
        {
            doOnce2 = true;
            collectP--;
            StartCoroutine(animCollect());
        }
    }

    IEnumerator animCollect()
    {
        if (animHud.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !animHud.IsInTransition(0))
        {
            animHud.SetTrigger("active");
        }
        yield return new WaitForSeconds(1f);
        collezionabileT++;
        yield return new WaitForSeconds(0.5f);
        doOnce2 = false;
    }
}
