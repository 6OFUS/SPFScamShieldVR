using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InformalJobUIManager : UIManager
{
    public Sprite[] stickers;

    public GameObject whatsupHomeButton;

    public GameObject homeScreen;

    public GameObject websiteHomeScreen;

    public GameObject amailScreen;

    public GameObject winScreen;
    public GameObject loseJoblessScreen;
    public GameObject mightNotBeScamScreen;

    private GameObject uiCanvas;

    public override IEnumerator FlashEffect()
    {
        yield return base.FlashEffect();
        whatsupHomeButton.SetActive(true);
        returnText.SetActive(true);


        ResetHomeButtons();
    }

    public override void ResetHomeButtons()
    {
        foreach (Button homeButton in homeButtons)
        {
            homeButton.gameObject.SetActive(true);
            homeButton.onClick.RemoveAllListeners();
            homeButton.onClick.AddListener(() =>
            {
                foreach (GameObject canvas in scenarioController.uiCanvas)
                {
                    if (canvas.activeInHierarchy)
                    {
                        uiCanvas = canvas;
                        break;
                    }
                }
                DisableAllCanvasChildren(uiCanvas);
                homeScreen.SetActive(true);
                returnText.SetActive(false);
            });
        }
    }

    private void Start()
    {
        foreach (Button homeButton in homeButtons)
        {
            homeButton.onClick.AddListener(() =>
            {
                if (homeScreen.activeInHierarchy)
                {
                    inkManager.choiceContainer.gameObject.SetActive(false);
                }
            });
        }
    }
}
