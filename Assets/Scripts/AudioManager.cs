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
    /// <summary>
    /// Audio source
    /// </summary>
    public AudioSource audioSource;
    /// <summary>
    /// Tap action button audio
    /// </summary>
    public AudioClip tapActionButton;

    /// <summary>
    /// Screenshot audio
    /// </summary>
    [Header("Scamshield")]
    public AudioClip screenshotClip;

    /// <summary>
    /// Crying audio
    /// </summary>
    [Header("Ending")]
    public AudioClip cryingClip;
    /// <summary>
    /// Lose ending audio
    /// </summary>
    public AudioClip loseClip;
    /// <summary>
    /// Win ending audio
    /// </summary>
    public AudioClip winClip;

    /// <summary>
    /// Function to play audio
    /// </summary>
    /// <param name="audioClip">Audio clip to be played</param>
    public void PlayAudio(AudioClip audioClip)
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }
}
