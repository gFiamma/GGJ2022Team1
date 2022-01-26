using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
	public static bool InMenU = false;											//setto la variabile booleana per capire se si è in pausa o no
	public GameObject MenuUI, OptionMenuUI;                                        //passo l'intero HUD della pausa
	public GameObject FirstButton, OptionsFirstButton, OptionsClosedButton;             //prendo in reference dei bottoni presenti nella pausa
	public GameObject video;
	public float tempo;
	public GameObject sfondoAnim;
	public Animator animSfondo;
	void Start()
	{
		StartCoroutine(Inizio());
		Cursor.lockState = CursorLockMode.Locked;                           //il cursore diventa invisibile e bloccato al centro
		Cursor.visible = false;                   
	}
	void Update()
	{

	}

	public void LoadLevel(string SceneName)
	{
		SceneManager.LoadScene(SceneName);                                    //si passa al menu principale
	}

	public void QuitGame()
	{
		Inventory.Reset();
		Application.Quit();                                                 //viene chiuso il gioco
	}
	public void BackOptions()
	{
		EventSystem.current.SetSelectedGameObject(OptionsClosedButton);                         //viene selezionato il tasto opzioni quando si torna indietro dal pannello delle opzioni
		OptionMenuUI.SetActive(false);															//l'intero pannello delle opzioni viene disattivato
		MenuUI.SetActive(true);																	//l'intero pannello del menu viene attivato
	}
	public void Options()
	{
		OptionMenuUI.SetActive(true);															//l'intero pannello delle opzioni viene attivato
		MenuUI.SetActive(false);																//l'intero pannello del menu viene disattivato
		EventSystem.current.SetSelectedGameObject(OptionsFirstButton);							//viene selezionato il tasto indietro quando si va nel pannello opzioni
	}

	IEnumerator Inizio()
	{
		video.SetActive(true);
		yield return new WaitForSeconds(tempo);                                 //attendo il tempo indicato dallo sviluppatore nell'inspector
		video.SetActive(false);
		StartCoroutine(stopVideo());                //https://pixabay.com/it/videos/intro-digitale-dj-51803/ video intro
	}
	IEnumerator stopVideo()
    {
		sfondoAnim.SetActive(true);
		animSfondo.SetTrigger("active");
		yield return new WaitForSeconds(1f);
		sfondoAnim.SetActive(false);
		EventSystem.current.SetSelectedGameObject(null);                    //nell'event system nulla è selezionato
		EventSystem.current.SetSelectedGameObject(FirstButton);             //il primo bottone è selezionato dall'event system
		MenuUI.SetActive(true);												//pannello menu attivo
	}
}
