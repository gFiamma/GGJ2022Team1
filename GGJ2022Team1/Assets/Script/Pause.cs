using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Pause : MonoBehaviour
{
	public static bool GameIsPaused = false;											//setto la variabile booleana per capire se si è in pausa o no
	public GameObject pauseMenuUI, OptionMenuUI;                                        //passo l'intero HUD della pausa
	public GameObject FirstButton, OptionsFirstButton, OptionsClosedButton;             //prendo in reference dei bottoni presenti nella pausa
	public GameObject HUDcanvas;                                                        //prendo in reference l'intero pannello che contiene L'HUD
	public GameObject DialogueBox;                                                        //prendo in reference il Box di Dialogo
	bool mouseUsed = default;															//una variabile che serve per capire se si passa dal controller ai comandi per pc

	void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;							//il cursore diventa invisibile e bloccato al centro
		Cursor.visible = false;												
	}
	void Update()
	{
		float horizontal = Input.GetAxisRaw("Horizontal");					//assegno ad un float il valore dell'asse orizzontale
		float vertical = Input.GetAxisRaw("Vertical");                      //assegno ad un float il valore dell'asse verticale

		if (GameIsPaused)                                                   //se il gioco va in pausa
		{
			if (ControllerCheck.controllerPlugged == true)					//se il controller è collegato
			{
				if (horizontal != 0 && mouseUsed == true || vertical != 0 && mouseUsed == true)			//se il valore delle assi è diverso da 0 ed il mouse è stato utilizzato
				{
					mouseUsed = false;											//setto la variabile a false, dato che ha fatto il suo dovere
					EventSystem.current.SetSelectedGameObject(FirstButton);		//faccio selezionare all'event system il FirstButton
				}
				Cursor.lockState = CursorLockMode.Locked;					//blocco il mouse e lo rendo invisibile
				Cursor.visible = false;
			}
			else															//sennò
			{
				mouseUsed = true;											//il mouse è in uso
				EventSystem.current.SetSelectedGameObject(null);			//nessun gameObject è selezionato dall'event system
				Cursor.lockState = CursorLockMode.None;						//il mouse è visibile e libero
				Cursor.visible = true;
			}
		}

		if (Input.GetButtonDown("Cancel") && !PlayerController.isDead && Inventory.vite > 0)		//se viene premuto il tasto per la pausa e lo Shop non è attivo
		{
			if (GameIsPaused)                                               //ed il gioco è già in pausa
			{
				Resume();                                                   //viene richiamata la funzione resume
				Cursor.lockState = CursorLockMode.Locked;                   //il cursore diventa invisibile e bloccato al centro
				Cursor.visible = false;
			}
			else                                                            //sennò
			{
				Paused();                                                    //viene richiamata la funziona pause
			}
		}
	}

	void Paused()
	{
		HUDcanvas.SetActive(false);                                         //disattivo l'hud
		DialogueBox.SetActive(false);                                         //disattivo il dialogue box
		pauseMenuUI.SetActive(true);                                        //l'intero pannello della pausa viene attivato
		Time.timeScale = 0f;                                                //il tempo del gioco viene congelato
		GameIsPaused = true;                                                //la booleana che setta la pausa diventa true
		EventSystem.current.SetSelectedGameObject(null);					//nell'event system nulla è selezionato
		EventSystem.current.SetSelectedGameObject(FirstButton);             //il primo bottone è selezionato dall'event system
		//cambio musiche

	}

	public void Resume()
	{
		HUDcanvas.SetActive(true);                                         //riattivo l'hud
        if (DialogueManager.isTyping)
        {
			DialogueBox.SetActive(true);                                         //riattivo il dialogue box
		}
		pauseMenuUI.SetActive(false);                                       //l'intero HUD della pausa viene reso falso
		OptionMenuUI.SetActive(false);                                       //l'intero HUD della pausa viene reso falso
		Time.timeScale = 1f;                                                //il tempo torna alla normalità
		StartCoroutine(waitJump());                                         //dato che dava problemi che saltava appena finiva il dialogo, ho deciso di fare così per evitare problemi
		//cambio musiche
		//Cursor.lockState = CursorLockMode.Locked;                           //il cursore diventa invisibile e bloccato al centro
		//Cursor.visible = false;
	}

	public void LoadMenu()
	{
		Inventory.Reset();
		Resume();                                                           //viene richiamata la funzione resume
		SceneManager.LoadScene("StartScene");                                    //si passa al menu principale
	}

	public void QuitGame()
	{
		Inventory.Reset();
		Application.Quit();                                                 //viene chiuso il gioco
	}
	public void BackOptions()
	{
		EventSystem.current.SetSelectedGameObject(OptionsClosedButton);                       //viene selezionato il tasto opzioni quando si torna indietro dal pannello delle opzioni
	}
	public void Options()
	{
		EventSystem.current.SetSelectedGameObject(OptionsFirstButton);                      //viene selezionato il tasto indietro quando si va nel pannello opzioni
	}

	IEnumerator waitJump()
	{
		yield return new WaitForSeconds(0.1f);
		GameIsPaused = false;                                                   //ridà al player il potere di muoversi
	}
}
