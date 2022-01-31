using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
    public static bool zPressed = default;
    public static bool xPressed = default;
    public static bool cPressed = default;
    public static bool aPressed = default;

    public static float suPressed = default;
    public static float giuPressed = default;
    public static float destraPressed = default;
    public static float sinistraPressed = default;

    private void Start()
    {
        zPressed = false;
        xPressed = false;
        cPressed = false;
    }
    public void Z()
    {
        zPressed = true;
    }

    public void X()
    {
        xPressed = true;
    }

    public void C()
    {
        cPressed = true;
    }
    public void A()
    {
        aPressed = true;
    }

    public void Su()
    {
        suPressed = 1f;
    }

    public void Su2()
    {
        suPressed = 0f;
    }
    public void Giu()
    {
        giuPressed = 1f;
    }
    public void Giu2()
    {
        giuPressed = 0f;
    }
    public void Destra()
    {
        destraPressed = 1f;
    }
    public void Destra2()
    {
        destraPressed = 0f;
    }
    public void Sinistra()
    {
        sinistraPressed = 1f;
    }
    public void Sinistra2()
    {
        sinistraPressed = 0f;
    }
}
