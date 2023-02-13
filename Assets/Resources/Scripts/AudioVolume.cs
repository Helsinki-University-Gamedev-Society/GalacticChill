using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioVolume : MonoBehaviour
{
    void Start()
    {
        NewVolume();
    }

    public void NewVolume()
    {
        GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("volume",0.8f);
    }
}
