using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Dialogue
{
    [SerializeField]
    private List<string> lines = default;               //creo una lista di string
    [SerializeField]
    private string Name = default;
    [SerializeField]
    private Sprite Face = default;


    public string name
    {
        get { return Name; }
    }
    public Sprite face
    {
        get { return Face; }
    }

    public List<string> Lines
    {
        get { return lines; }                          
    }
}
