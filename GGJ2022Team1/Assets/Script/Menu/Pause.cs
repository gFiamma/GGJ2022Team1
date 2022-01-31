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
	public GameObject DialogueBox;                                                        //prendo in reference il Box di Dialogo

	void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;							//il cursore diventa invisibile e bloccato al centro
		Cursor.visible = false;
	}
	void Update()
	{
		if (GameIsPaused)                                                   //se il gioco va in pausa
		{
			Cursor.lockState = CursorLockMode.Locked;					//blocco il mouse e lo rendo invisibile
			Cursor.visible = false;
		}

		if (Controls.cPressed && !PlayerController.isDead && Inventory.vite > 0)		//se viene premuto il tasto per la pausa e lo Shop non è attivo
		{
			Controls.cPressed = false;
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
		DialogueBox.SetActive(false);                                         //disattivo il dialogue box
		pauseMenuUI.SetActive(true);                                        //l'intero pannello della pausa viene attivato
		Time.timeScale = 0f;                                                //il tempo del gioco viene congelato
		GameIsPaused = true;                                                //la booleana che setta la pausa diventa true
        if (Teleport.isRealWorld)
        {
			AudioManager.AudioList[2].Play();
			AudioManager.AudioList[1].Stop();
        }
        else
        {
			AudioManager.AudioList[2].Play();
			AudioManager.AudioList[0].Stop();
		}
	}

	public void Resume()
	{
        if (DialogueManager.isTyping)
        {
			DialogueBox.SetActive(true);                                         //riattivo il dialogue box
		}
		pauseMenuUI.SetActive(false);                                       //l'intero HUD della pausa viene reso falso
		OptionMenuUI.SetActive(false);                                       //l'intero HUD della pausa viene reso falso
		Time.timeScale = 1f;                                                //il tempo torna alla normalità
		StartCoroutine(waitJump());                                         //dato che dava problemi che saltava appena finiva il dialogo, ho deciso di fare così per evitare problemi
		if (Teleport.isRealWorld)
		{
			AudioManager.AudioList[1].Play();
			AudioManager.AudioList[2].Stop();
		}
		else
		{
			AudioManager.AudioList[0].Play();
			AudioManager.AudioList[2].Stop();
		}
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
	}
	public void Options()
	{
	}

	IEnumerator waitJump()
	{
		yield return new WaitForSeconds(0.1f);
		GameIsPaused = false;                                                   //ridà al player il potere di muoversi
	}
}
