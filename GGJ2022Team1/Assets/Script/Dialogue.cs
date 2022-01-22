using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    [SerializeField]
    private List<string> lines = default;               //creo una lista di string

    public List<string> Lines
    {
        get { return lines; }                          
    }
}
