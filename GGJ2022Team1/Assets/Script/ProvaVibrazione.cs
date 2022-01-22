using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure; //Serve per la vibrazione del controller

public class ProvaVibrazione : MonoBehaviour            //Codice per testare la vibrazione per ogni singolo tasto del controller (bisogna attivare lo script assegnato al Player)
{
    //servono per la vibrazione del controller
    PlayerIndex playerIndex;
    GamePadState state;
    GamePadState prevState;

    void Update()
    {
        if(Input.GetButtonDown("Jump"))
        {
            GamePad.SetVibration(playerIndex, .3f, .3f);                 //IL CONTROLLER VIBRA
            StartCoroutine("StopVibrate");
            Debug.Log("A");
        }

        if (Input.GetButtonDown("Back"))
        {
            GamePad.SetVibration(playerIndex, .3f, .3f);                 //IL CONTROLLER VIBRA
            StartCoroutine("StopVibrate");
            Debug.Log("B");
        }

        if (Input.GetButtonDown("Interact"))
        {
            GamePad.SetVibration(playerIndex, .3f, .3f);                 //IL CONTROLLER VIBRA
            StartCoroutine("StopVibrate");
            Debug.Log("X");
        }

        if (Input.GetButtonDown("Spell"))
        {
            GamePad.SetVibration(playerIndex, .3f, .3f);                 //IL CONTROLLER VIBRA
            StartCoroutine("StopVibrate");
            Debug.Log("Y");
        }

        if (Input.GetButtonDown("Cancel"))
        {
            GamePad.SetVibration(playerIndex, .3f, .3f);                 //IL CONTROLLER VIBRA
            StartCoroutine("StopVibrate");
            Debug.Log("Start");
        }

        if (Input.GetButtonDown("Select"))
        {
            GamePad.SetVibration(playerIndex, .3f, .3f);                 //IL CONTROLLER VIBRA
            StartCoroutine("StopVibrate");
            Debug.Log("Select");
        }

        if (Input.GetButtonDown("Run"))
        {
            GamePad.SetVibration(playerIndex, .3f, .3f);                 //IL CONTROLLER VIBRA
            StartCoroutine("StopVibrate");
            Debug.Log("RB");
        }

        if (Input.GetButtonDown("Run2"))
        {
            GamePad.SetVibration(playerIndex, .3f, .3f);                 //IL CONTROLLER VIBRA
            StartCoroutine("StopVibrate");
            Debug.Log("LB");
        }

        if (Input.GetButtonDown("leftStick"))
        {
            GamePad.SetVibration(playerIndex, .3f, .3f);                 //IL CONTROLLER VIBRA
            StartCoroutine("StopVibrate");
            Debug.Log("analogico sinistra premuto");
        }

        if (Input.GetButtonDown("rightStick"))
        {
            GamePad.SetVibration(playerIndex, .3f, .3f);                 //IL CONTROLLER VIBRA
            StartCoroutine("StopVibrate");
            Debug.Log("analogicoSX destra premuto");
        }

        var movement = Input.GetAxis("Horizontal");
        if(movement >0 || movement < 0)
        {
            GamePad.SetVibration(playerIndex, .3f, .3f);                 //IL CONTROLLER VIBRA
            StartCoroutine("StopVibrate");
            Debug.Log("analogicoSX destra e sinistra");
        }

        var movement2 = Input.GetAxis("Vertical");
        if (movement2 > 0 || movement2 < 0)
        {
            GamePad.SetVibration(playerIndex, .3f, .3f);                 //IL CONTROLLER VIBRA
            StartCoroutine("StopVibrate");
            Debug.Log("analogico sopra e sotto");
        }

        var triggers = Input.GetAxis("Aim");
        if (triggers > 0 || triggers < 0)
        {
            GamePad.SetVibration(playerIndex, .3f, .3f);                 //IL CONTROLLER VIBRA
            StartCoroutine("StopVibrate");
            if(triggers >0)
            {
                Debug.Log("RT");
            }
            else
            {
                Debug.Log("LT");
            }

        }


        var xArrow = Input.GetAxis("xArrow");
        if (xArrow > 0 || xArrow < 0)
        {
            GamePad.SetVibration(playerIndex, .3f, .3f);                 //IL CONTROLLER VIBRA
            StartCoroutine("StopVibrate");
            Debug.Log("Frecce destra e sinistra");
        }

        var yArrow = Input.GetAxis("yArrow");
        if (yArrow > 0 || yArrow < 0)
        {
            GamePad.SetVibration(playerIndex, .3f, .3f);                 //IL CONTROLLER VIBRA
            StartCoroutine("StopVibrate");
            Debug.Log("Frecce sopra e sotto");
        }

        var mouseX = Input.GetAxis("MouseX");
        if (mouseX > 0 || mouseX < 0)
        {
            GamePad.SetVibration(playerIndex, .3f, .3f);                 //IL CONTROLLER VIBRA
            StartCoroutine("StopVibrate");
            Debug.Log("analogicoDX destra e sinistra");
        }

        var mouseY = Input.GetAxis("MouseY");
        if (mouseY > 0 || mouseY < 0)
        {
            GamePad.SetVibration(playerIndex, .3f, .3f);                 //IL CONTROLLER VIBRA
            StartCoroutine("StopVibrate");
            Debug.Log("analogicoDX sopra e sotto");
        }

    }

    IEnumerator StopVibrate()
    {
        yield return new WaitForSeconds(0.3f);
        GamePad.SetVibration(playerIndex, 0f, 0f);                  //IL CONTROLLER SMETTE DI VIBRARE
    }
}
