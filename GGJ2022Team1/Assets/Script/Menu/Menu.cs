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
	public AudioSource Musichetta;

	public Text dangoText;
	public Text dangoText2;
	void Start()
	{
		StartCoroutine(Inizio());
		Cursor.lockState = CursorLockMode.Locked;                           //il cursore diventa invisibile e bloccato al centro
		Cursor.visible = false;
		animSfondo.SetBool("active", false);
	}
	void Update()
	{
		dangoText.text = "You collected " + Inventory.collezionabile + "/15 Dangos.";
		dangoText2.text = "You collected " + Inventory.collezionabile + "/15 Dangos.";
	}

	public void LoadLevel(string SceneName)
	{
		Inventory.Reset();
		SceneManager.LoadScene(SceneName);                                    //si passa al menu principale
	}

	public void QuitGame()
	{
		Inventory.Reset();
		Application.Quit();                                                 //viene chiuso il gioco
	}
	public void BackOptions()
	{
		OptionMenuUI.SetActive(false);															//l'intero pannello delle opzioni viene disattivato
		MenuUI.SetActive(true);																	//l'intero pannello del menu viene attivato
	}
	public void Options()
	{
		OptionMenuUI.SetActive(true);															//l'intero pannello delle opzioni viene attivato
		MenuUI.SetActive(false);																//l'intero pannello del menu viene disattivato
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
		animSfondo.SetBool("active", true);
		Musichetta.Play();
		yield return new WaitForSeconds(1f);
		animSfondo.SetBool("active", false);
		yield return new WaitForSeconds(1f);
		sfondoAnim.SetActive(false);
		MenuUI.SetActive(true);												//pannello menu attivo
	}
}
