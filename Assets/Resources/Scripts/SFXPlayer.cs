using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPlayer : MonoBehaviour
{
    private AudioSource audioSource;
    
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PlaySFX("crashon");
    }

    public void PlaySFX(string clipName)
    {
        AudioClip clip = Resources.Load<AudioClip>("Audio/"+clipName);
        audioSource.PlayOneShot(clip);
    }
}
