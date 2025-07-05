using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class Fade : MonoBehaviour
{
    public GameObject player;
    public Transform tpPoint;

    public Image fadeImage;
    public float fadeDuration = 2f;

    public RecapVideo recapVideo;

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

    public void FadeIn()
    {
        StartCoroutine(Transition(0f));
    }

    public void FadeOut()
    {
        StartCoroutine(Transition(1f));
    }

    public IEnumerator FadeTeleport(VideoClip videoClip)
    {
        // Step 1: Fade Out (wait until fully black)
        yield return StartCoroutine(Transition(1f));

        // Step 2: Teleport
        player.transform.position = tpPoint.position;
        player.transform.rotation = tpPoint.rotation;

        // Step 3: Fade In (wait until transparent)
        yield return StartCoroutine(Transition(0f));
        recapVideo.PlayVideo(videoClip);
    }



    // Start is called before the first frame update
    void Start()
    {
        FadeIn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
