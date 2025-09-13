/*
    Author: Kevin Heng
    Date: 04/08/2025
    Description: The InvestmentScamUIManager class is used to manage all UI related functions to the investment scam scenario
*/
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvestmentScamUIManager : UIManager
{
    /// <summary>
    /// Reference InvestmentScamManager script
    /// </summary>
    public InvestmentScamManager scamManager;
    /// <summary>
    /// Reference InvestmentScenariosAudioManager script
    /// </summary>
    public InvestmentScenariosAudioManager audioManager;

    /// <summary>
    /// Kachagram chat screen
    /// </summary>
    [Header("Kachagram")]
    public GameObject kachagramScreen;
    /// <summary>
    /// Rechel's Kachagram profile picture
    /// </summary>
    public GameObject rachelKachagramPfp;
    /// <summary>
    /// Kachagram account missing screen
    /// </summary>
    public GameObject kachagramAccountMissingScreen;

    /// <summary>
    /// App shop main screen
    /// </summary>
    [Header("AppShop")]
    public GameObject appShopScreen;
    /// <summary>
    /// Open EZProfit app details in App Shop
    /// </summary>
    public GameObject eZProfitAppScreen;
    /// <summary>
    /// EZProfit app downloading screen
    /// </summary>
    public GameObject eZProfitAppDownloadingScreen;
    /// <summary>
    /// EZProfit downloaded screen
    /// </summary>
    public GameObject eZProfitAppDownloadedScreen;
    /// <summary>
    /// EZZProfit in app shop scoll rect
    /// </summary>
    public ScrollRect eZProfitAppShopScrollRect;
    /// <summary>
    /// EZProfit in app shop scroll duration
    /// </summary>
    public float scrollDuration;

    /// <summary>
    /// EZProfit sign up screen
    /// </summary>
    [Header("EZProfit app")]
    public GameObject eZProfitSignUpScreen;
    /// <summary>
    /// EZProfit account details screen
    /// </summary>
    public GameObject eZProfitAccountDetailsScreen;
    /// <summary>
    /// EZProfit home screen
    /// </summary>
    public GameObject eZProfitHomeScreen;

    /// <summary>
    /// EZProfit BeiBei screen
    /// </summary>
    [Header("EZProfit Investing")]
    public GameObject eZProfitBeiBeiScreen;
    /// <summary>
    /// EZProfit invest screen
    /// </summary>
    public GameObject eZProfitInvestScreen;
    /// <summary>
    /// EZProfit input amount to invest screen
    /// </summary>
    public GameObject eZProfitAmountInputScreen;

    /// <summary>
    /// EZProfit add payment method screen
    /// </summary>
    [Header("EZProfit Credit Card")]
    public GameObject eZProfitAddPaymentMethodScreen;
    /// <summary>
    /// EZProfit credit card details screen
    /// </summary>
    public GameObject eZProfitCreditCardDetailsScreen;
    /// <summary>
    /// EZProfit scanning credit card screen
    /// </summary>
    public GameObject eZProfitScanningCreditCardScreen;
    /// <summary>
    /// EZProfit credit card details added screen
    /// </summary>
    public GameObject eZProfitCreditCardDetailsAddedScreen;
    /// <summary>
    /// EZProfit existing card screen
    /// </summary>
    public GameObject eZProfitExistingCardScreen;
    /// <summary>
    /// EZProfit loading card screen
    /// </summary>
    public GameObject eZProfitLoadingCardScreen;
    /// <summary>
    /// EZProfit invesment success screen
    /// </summary>
    public GameObject eZProfitInvestmentSuccessScreen;
    /// <summary>
    /// EZProfit earnings screen
    /// </summary>
    public GameObject eZProfitEarningsScreen;

    /// <summary>
    /// EZProfit withdraw loading screen
    /// </summary>
    [Header("EZProfit Withdraw")]
    public GameObject eZProfitWithdrawLoadingScreen;
    /// <summary>
    /// EZProfit withdraw denied screen
    /// </summary>
    public GameObject eZProfitWithdrawDeniedScreen;

    /// <summary>
    /// Change UI to show Kachagram chat screen after selecting option to open phone notification
    /// </summary>
    public void ScammerNotification()
    {
        kachagramScreen.SetActive(true);
        StartCoroutine(scamManager.WaitAndContinueStory(0, audioManager));
    }

    /// <summary>
    /// Add Rechel's Kachgram profile picture to chat scroll view
    /// </summary>
    /// <param name="index"></param>
    public void AddProfilePicture(int index)
    {
        rachelKachagramPfp.transform.SetParent(scenarioController.messageContentParent);
        rachelKachagramPfp.transform.SetAsFirstSibling();
        string selectedText = scamManager.playerChoices[index].choiceName;
        scamManager.messagingSystem.PlayerNextMessage(selectedText);
        StartCoroutine(scamManager.WaitAndContinueStory(scamManager.messageTime, audioManager));
    }

    /// <summary>
    /// Downloading EZProfit animation
    /// </summary>
    /// <returns></returns>
    public IEnumerator DownloadingEZProfit()
    {
        yield return new WaitForSeconds(1);
        eZProfitAppDownloadingScreen.SetActive(true);
        yield return new WaitForSeconds(loadingTime);
        StartCoroutine(scamManager.WaitAndContinueStory(0, audioManager));
        eZProfitAppDownloadedScreen.SetActive(true);
    }

    /// <summary>
    /// Download EZProfit process from app shop to opening app
    /// </summary>
    /// <returns></returns>
    public IEnumerator DownloadEZProfitAnimation()
    {
        appShopScreen.SetActive(true);
        yield return new WaitForSeconds(loadingTime);

        eZProfitAppScreen.SetActive(true);   
        eZProfitAppShopScrollRect.normalizedPosition = new Vector2(0, 1);
        eZProfitAppShopScrollRect.DONormalizedPos(new Vector2(0, 0), scrollDuration).SetEase(Ease.Linear);
        yield return new WaitForSeconds(scrollDuration);
        eZProfitAppShopScrollRect.normalizedPosition = new Vector2(0, 1);

        yield return StartCoroutine(DownloadingEZProfit());

        eZProfitSignUpScreen.SetActive(true);
    }

    /// <summary>
    /// Creating EZProfit account process
    /// </summary>
    /// <returns></returns>
    public IEnumerator CreateEZProfitAccount()
    {
        eZProfitAccountDetailsScreen.SetActive(true);
        yield return new WaitForSeconds(loadingTime);

        eZProfitHomeScreen.SetActive(true);
        yield return new WaitForSeconds(loadingTime);

        DisableAllCanvasChildren();
        kachagramScreen.SetActive(true);
        StartCoroutine(scamManager.WaitAndContinueStory(1, audioManager));
    }

    /// <summary>
    /// Change UI to show EZProfit home screen after selecting option to open the app
    /// </summary>
    public void OpenEZProfitApp()
    {
        eZProfitHomeScreen.SetActive(true);
        StartCoroutine(scamManager.WaitAndContinueStory(scamManager.messageTime, audioManager));
    }

    /// <summary>
    /// Wait for credit card to be scanned
    /// </summary>
    /// <returns></returns>
    public IEnumerator ScanCreditCard()
    {
        yield return new WaitForSeconds(loadingTime);
        eZProfitScanningCreditCardScreen.SetActive(true);
        yield return new WaitForSeconds(loadingTime);
        eZProfitCreditCardDetailsAddedScreen.SetActive(true);
    }

    /// <summary>
    /// Select scanned credit card
    /// </summary>
    /// <returns></returns>
    public IEnumerator PickCard()
    {
        eZProfitLoadingCardScreen.SetActive(true);
        yield return new WaitForSeconds(loadingTime);
        eZProfitInvestmentSuccessScreen.SetActive(true);
        scamManager.isMoneyInvested = true;
        StartCoroutine(scamManager.WaitAndContinueStory(scamManager.messageTime, audioManager));
    }

    /// <summary>
    /// Investing in Bei Bei animation
    /// </summary>
    /// <returns></returns>
    public IEnumerator InvestAnimation()
    {
        eZProfitBeiBeiScreen.SetActive(true);
        yield return new WaitForSeconds(loadingTime);

        eZProfitInvestScreen.SetActive(true);
        yield return new WaitForSeconds(loadingTime);

        eZProfitAmountInputScreen.SetActive(true);
        yield return new WaitForSeconds(loadingTime);

        eZProfitAddPaymentMethodScreen.SetActive(true);
        yield return new WaitForSeconds(loadingTime);

        eZProfitCreditCardDetailsScreen.SetActive(true);
        yield return StartCoroutine(ScanCreditCard());

        eZProfitCreditCardDetailsScreen.SetActive(true);
        yield return new WaitForSeconds(loadingTime);

        eZProfitExistingCardScreen.SetActive(true);
        yield return StartCoroutine(PickCard());
    }

    /// <summary>
    /// Change UI to show earnings screen after selecting option to withdraw money
    /// </summary>
    public void EarningsScreen()
    {
        eZProfitEarningsScreen.SetActive(true);
        StartCoroutine(scamManager.WaitAndContinueStory(scamManager.messageTime, audioManager));
    }

    /// <summary>
    /// Waiting for earnings to be withdrawn
    /// Shows error after waiting
    /// </summary>
    /// <returns></returns>
    public IEnumerator WithdrawEarnings()
    {
        eZProfitWithdrawLoadingScreen.SetActive(true);
        yield return new WaitForSeconds(loadingTime);
        eZProfitWithdrawDeniedScreen.SetActive(true);
        StartCoroutine(scamManager.WaitAndContinueStory(scamManager.messageTime, audioManager));
    }

    /// <summary>
    /// Return to Kachagram chat to ask Rachel why money cannot be withdrawn
    /// </summary>
    public void AskRachel()
    {
        DisableAllCanvasChildren();
        kachagramScreen.SetActive(true);
        StartCoroutine(scamManager.WaitAndContinueStory(1, audioManager));
    }

    /// <summary>
    /// Change UI to lose screen after selecting final option
    /// </summary>
    /// <returns></returns>
    public IEnumerator HandleLoseEnding()
    {
        kachagramAccountMissingScreen.SetActive(true);
        audioManager.PlayAudio(audioManager.cryingClip);
        yield return new WaitForSeconds(2);
        scenarioController.scenarioCanvas.SetActive(false);
        loseScreen.SetActive(true);
        audioManager.audioSource.clip = audioManager.loseClip;
        audioManager.audioSource.Play();
        scamManager.ClearChoices(scamManager.choiceContainer);
        Destroy(scamManager.scamshieldButton);
        yield return new WaitForEndOfFrame();
        whatHappenButton.onClick.AddListener(() =>
        {
            scamManager.recapVideoScript.PlayVideo(scamManager.whatHappenLoseVideoClip);
        });
        scamManager.ProceedToVideo(scamManager.gameOverVideoClip);
    }
}
