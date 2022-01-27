using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource[] PVTAudioList;

    public static AudioSource[] AudioList;
    void Awake()
    {
        AudioList = PVTAudioList;
    }
}
