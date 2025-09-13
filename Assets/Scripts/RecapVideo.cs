/*
    Author: Kevin Heng
    Date: 9/07/2025
    Description: The RecapVideo class is used to manage the functions for the ending recap videos
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class RecapVideo : MonoBehaviour
{
    /// <summary>
    /// Video player for recap video
    /// </summary>
    public VideoPlayer videoPlayer;

    /// <summary>
    /// Play recap video
    /// </summary>
    /// <param name="videoClip"></param>
    public void PlayVideo(VideoClip videoClip)
    {
        videoPlayer.clip = videoClip;
        videoPlayer.Play();
    }
}
