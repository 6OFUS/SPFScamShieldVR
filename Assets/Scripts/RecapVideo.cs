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
    public VideoPlayer videoPlayer;

    public void PlayVideo(VideoClip videoClip)
    {
        videoPlayer.clip = videoClip;
        videoPlayer.Play();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
