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

public class UIManager : MonoBehaviour
{
    public bool screenshotTaken;

    public Image flashImage; 
    public float flashDuration;

    public InkManager inkManager;
    public ScenarioController scenarioController;

    public List<Button> homeButtons = new List<Button>();

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip screenshotClip;

    [Header("Scamshield UI")]
    public GameObject scamshieldScreen;

    [Header("Instructions UI")]
    public GameObject returnText;

    [Header("App UI")]
    public Button whatsupAppButton;

    public void Screenshot()
    {
        Debug.Log("Screenshot");
        if (!screenshotTaken)
        {
            inkManager.stopStory = true;
            screenshotTaken = true;
            audioSource.clip = screenshotClip;
            audioSource.Play();
            flashImage.gameObject.SetActive(true);
            StartCoroutine(FlashEffect());
            whatsupAppButton.enabled = false;
        }
    }

    public void OpenScamShieldApp()
    {
        if (screenshotTaken)
        {
            scamshieldScreen.SetActive(true);
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

    public void DisableAllCanvasChildren(GameObject parent)
    {
        foreach (Transform child in parent.transform)
        {
            Debug.Log(child.name);
            child.gameObject.SetActive(false);
        }
    }

    public virtual void ResetHomeButtons()
    {

    }
}
