/*
    Author: Kevin Heng
    Date: 23/06/2025
    Description: The UIManager class is used to manage UI related functions
*/
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class UIManager : MonoBehaviour
{
    public float loadingTime;

    [Header("Home")]
    public GameObject homeScreen;

    [Header("Screenshot")]
    public bool screenshotTaken;
    public Image flashImage; 
    public float flashDuration;

    public GameObject frontFacingCamera;
    public GameObject homeButtonLine;

    [Header("Scamshield UI")]
    public GameObject scamshieldScreen;
    public GameObject scamshieldLoadingScreen;

    [Header("End screen UI")]
    public Button whatHappenButton;
    public GameObject whatShouldYouDoButton;
    public Transform whatShouldYouDoButtonPos;
    public GameObject winScreen;
    public GameObject loseScreen;
    public GameObject reportAfterScammedScreen;

    [Header("Script references")]

    public ScenarioController scenarioController;

    public virtual void Screenshot(InkManager inkManager, AudioManager audioManager)
    {
        if (!screenshotTaken)
        {
            inkManager.stopStory = true;
            screenshotTaken = true;
            audioManager.audioSource.clip = audioManager.screenshotClip;
            audioManager.audioSource.Play();
            flashImage.gameObject.SetActive(true);
            StartCoroutine(FlashEffect());
        }
    }

    public virtual IEnumerator FlashEffect()
    {
        //Fade in
        float elapsed = 0f;
        while (elapsed < flashDuration / 2)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, elapsed / (flashDuration / 2));
            flashImage.color = new Color(1, 1, 1, alpha);
            yield return null;
        }

        //Fade out
        elapsed = 0f;
        while (elapsed < flashDuration / 2)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsed / (flashDuration / 2));
            flashImage.color = new Color(1, 1, 1, alpha);
            yield return null;
        }

        flashImage.color = new Color(1, 1, 1, 0);
        flashImage.gameObject.SetActive(false);
    }

    public void DisableAllCanvasChildren()
    {
        foreach (Transform child in scenarioController.scenarioCanvas.transform)
        {
            if(child.name == frontFacingCamera.name || child.name == homeButtonLine.name)
            {
                continue;
            }
            else
            {
                child.gameObject.SetActive(false);
            }
        }
    }
}
