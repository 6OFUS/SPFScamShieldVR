/*
    Author: Kevin Heng
    Date: 11/08/2025
    Description: The AudioManager class is used to handle all audio clips and the functions for playing them
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip tapActionButton;

    [Header("Scamshield")]
    public AudioClip screenshotClip;

    [Header("Ending")]
    public AudioClip cryingClip;
    public AudioClip loseClip;
    public AudioClip winClip;

    public void PlayAudio(AudioClip audioClip)
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }
}
