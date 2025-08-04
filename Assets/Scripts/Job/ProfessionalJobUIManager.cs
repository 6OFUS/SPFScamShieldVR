/*
    Author: Kevin Heng
    Date: 01/07/2025
    Description: The ProfessionalJobUIManager class is used to manage all UI related to the professional job ad scenario
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class ProfessionalJobUIManager : UIManager
{
    public GameObject whatShouldYouDoButton;

    [Header("Stickers")]
    public Sprite[] stickers;
    public GameObject stickerChoicePrefab;

    [Header("Phone screens")]
    public GameObject homeScreen;
    public GameObject whatsupScreen;
    public GameObject websiteHomeScreen;
    public GameObject amailScreen;
    public GameObject luciaEmailScreen;
    public GameObject websiteCareersScreen;




    public override IEnumerator FlashEffect()
    {
        yield return base.FlashEffect();
    }

    private void Start()
    {

    }
}
