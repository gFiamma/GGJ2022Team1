using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class qualitySetting : MonoBehaviour
{

    //https://www.youtube.com/watch?v=YOaYQrN1oYQ setting menu in unity from Brackeys

    Resolution[] resolutions;                                           //creo un array per tenere le varie risoluzioni
    public Dropdown resolutionDropdown;                                 //prendo in reference un dropdown per le risoluzioni                   
    public Dropdown qualityDropdown;                                    //prendo in reference un dropdown per le qualità    

    private void Start()
    {
        resolutions = Screen.resolutions;                               //assegno all'array, una risoluzione  

        resolutionDropdown.ClearOptions();                              //ripulisce il dropdown

        List<string> options = new List<string>();                      //creo una lista di string

        int currentResolutonIndex = 0;                                  //creo un index che terrà la mia risoluzione attuale
        for (int i = 0; i < resolutions.Length; i++)                    //creo un for che controlla tutte le risoluzioni possibili
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;         //crea una string che contine altezza e larghezza dello schermo
            options.Add(option);                                                        //la aggiunge alle opzioni del dropdown delle risoluzioni

            if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)      //se una di queste risoluzioni ha gli stessi identici valori della risoluzione corrente
            {
                currentResolutonIndex = i;                                                                                              //assegno a currentResolutionIndex il valore dell'indice in cui ha trovato quella risoluzione
            }
        }

        resolutionDropdown.AddOptions(options);                                                                                         //aggiunge le varie opzioni al dropdown delle risoluzioni
        resolutionDropdown.value = currentResolutonIndex;                                                                               //il value standard che ci sarà nel dropdown, sarà il value della nostra risoluzione corrente
        resolutionDropdown.RefreshShownValue();                                                                                         //fa un refresh del Dropdown per mostrare le modifiche

        int currentQualityIndex = 0;                                                                            //creo un int che terrà l'indice della qualità attuale che ha il gioco
        currentQualityIndex = QualitySettings.GetQualityLevel();                                                //ottengo l'indice della qualità attuale
        qualityDropdown.value = currentQualityIndex;                                                            //setto il value del dropdown delle qualità uguale al value della qualità attuale
        qualityDropdown.RefreshShownValue();                                                                    //fa un refresh del Dropdown per mostrare le modifiche
    }

    public void SetResolution (int resolutionIndex)                                                             //metodo che serve a settare la risoluzione dalle opzioni
    {
        Resolution resolution = resolutions[resolutionIndex];                                                   //prende l'indice del valore passato tramite il dropdown nelle opzioni
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);                           //lo assegna alla finestra di gioco
    }

    public void SetQuality(int qualityIndex)                                                                    //metodo che serve a settare la qualità dalle opzioni
    {
        QualitySettings.SetQualityLevel(qualityIndex);                                                          //setta la qualità del gioco pari all'indice passato dal dropdown nelle opzioni
    }

    public void SetFullScreen (bool isFullscreen)                                                               //metodo che serve a settare lo schermo intero dalle opzioni
    {
        Screen.fullScreen = isFullscreen;                                                                       //prende il valore del toggle che si trova nelle opzioni, e mette il gioco a schermo intero o finestra in base alla booleana se ritorna true o false
    }
}
