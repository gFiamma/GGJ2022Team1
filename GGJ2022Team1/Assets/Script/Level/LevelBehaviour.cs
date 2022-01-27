using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBehaviour : MonoBehaviour
{
    public GameObject player, spriteFantasy,spriteReal;
    public PlayerController FScript;
    public PlayerController2 RScript;

    void Start()
    {
        if(DialogueManager.Yes >= DialogueManager.No)
        {
            player.transform.position = new Vector3(-0.5f, 0.5f, 0);
            AudioManager.AudioList[0].volume = 0.4f;
            AudioManager.AudioList[0].Play();
            Teleport.isRealWorld = false;
        }
        else
        {
            player.transform.position = new Vector3(-0.5f, -26.5f, 0);
            AudioManager.AudioList[1].volume = 0.4f;
            AudioManager.AudioList[1].Play();
            Teleport.isRealWorld = true;
        }
    }

    private void Update()
    {
        if (!Teleport.isRealWorld)
        {
            spriteFantasy.SetActive(true);
            spriteReal.SetActive(false);
            FScript.enabled = true;
            RScript.enabled = false;
        }
        else
        {
            spriteFantasy.SetActive(false);
            spriteReal.SetActive(true);
            FScript.enabled = false;
            RScript.enabled = true;
        }
    }
}
