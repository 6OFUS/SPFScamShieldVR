/*
    Author: Kevin Heng
    Date: 9/07/2025
    Description: The Fade class is used to manage the functions for transitions between scenes
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class Fade : MonoBehaviour
{
    /// <summary>
    /// XR origin
    /// </summary>
    public GameObject player;
    /// <summary>
    /// Point where player will be teleported to
    /// </summary>
    public Transform tpPoint;

    /// <summary>
    /// Image used for fade in and out
    /// </summary>
    public Image fadeImage;
    /// <summary>
    /// Duration of fade
    /// </summary>
    public float fadeDuration = 2f;

    /// <summary>
    /// Reference RecapVideo script
    /// </summary>
    public RecapVideo recapVideo;

    /// <summary>
    /// Transition animation
    /// </summary>
    /// <param name="targetAlpha">Opacity of fade image</param>
    /// <returns></returns>
    public IEnumerator Transition(float targetAlpha)
    {
        Color startColor = fadeImage.color;
        float startAlpha = startColor.a;
        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, timer / fadeDuration);
            fadeImage.color = new Color(startColor.r, startColor.g, startColor.b, newAlpha);
            yield return null;
        }

        // Ensure it ends exactly at targetAlpha
        fadeImage.color = new Color(startColor.r, startColor.g, startColor.b, targetAlpha);
    }
    /// <summary>
    /// Function to fade into scene
    /// </summary>
    public void FadeIn()
    {
        StartCoroutine(Transition(0f));
    }
    /// <summary>
    /// Function to fade out of scene
    /// </summary>
    public void FadeOut()
    {
        StartCoroutine(Transition(1f));
    }
    /// <summary>
    /// Fade out then teleport player to teleport point
    /// </summary>
    /// <param name="videoClip"></param>
    /// <returns></returns>
    public IEnumerator FadeTeleport(VideoClip videoClip)
    {
        yield return StartCoroutine(Transition(1f));
        player.transform.position = tpPoint.position;
        player.transform.rotation = tpPoint.rotation;
        yield return StartCoroutine(Transition(0f));
        recapVideo.PlayVideo(videoClip);
    }



    // Start is called before the first frame update
    void Start()
    {
        FadeIn();
    }
}
