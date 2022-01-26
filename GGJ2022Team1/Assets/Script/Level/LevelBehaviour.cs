using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBehaviour : MonoBehaviour
{
    public GameObject player;
    void Start()
    {
        if(DialogueManager.Yes >= DialogueManager.No)
        {
            player.transform.position = new Vector3(-0.5f, 0.5f, 0);
        }
        else
        {
            player.transform.position = new Vector3(-0.5f, -26.5f, 0);
        }
    }
}
