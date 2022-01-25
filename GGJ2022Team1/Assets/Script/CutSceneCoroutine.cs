using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class CutSceneCoroutine : MonoBehaviour
{
    public float tempo = default;                                                                           //creo una variabile tempo, per settare dall'inspector quanto tempo deve passare prima di cambiare scena
    public GameObject primoTasto;                                                                           //prendo in reference il tasto per saltare la cutscene
    public string nomeScena = default;                                                                      //creo una string per settare nell'inspector in quale scena andare
    public string nomeScena2 = default;                                                                     //creo una string per settare nell'inspector in quale scena andare
    void Start()
    {
        EventSystem.current.SetSelectedGameObject(null);                                                    //nessun gameobject è selezionato dall'event system 
        EventSystem.current.SetSelectedGameObject(primoTasto);                                              //il gameobject "FirstButton" è selezionato dall'event system 
        StartCoroutine(Inizio());                                                                           //faccio partire una coroutine che farà cambiare scena
    }

    public void SaltaCutscene()
    {
        SceneManager.LoadScene(nomeScena2);                                    //viene caricata la scena assegnata alla variabile
    }

    IEnumerator Inizio()
    {
        yield return new WaitForSeconds(tempo);                                 //attendo il tempo indicato dallo sviluppatore nell'inspector
        SceneManager.LoadScene(nomeScena);                                      //viene caricata la scena assegnata alla variabile

    }
}
