/*
    Author: Kevin Heng
    Date: 04/08/2025
    Description: The InvestmentScamUIManager class is used to manage all UI related to the investment scam scenario
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvestmentScamUIManager : UIManager
{
    public InvestmentScamManager scamManager;

    [Header("Kachagram")]
    public GameObject kachagramScreen;
    public GameObject rachelKachagramPfp;
    public GameObject kachagramAccountMissingScreen;

    [Header("AppShop")]
    public GameObject appShopScreen;
    public GameObject eZProfitAppScreen;
    public GameObject eZProfitAppDownloadingScreen;
    public GameObject eZProfitAppDownloadedScreen;

    [Header("EZProfit app")]
    public GameObject eZProfitAppIcon;
    public GameObject eZProfitSignUpScreen;
    public GameObject eZProfitAccountDetailsScreen;
    public GameObject eZProfitHomeScreen;

    [Header("EZProfit Investing")]
    public GameObject eZProfitBaoBeiScreen;
    public GameObject eZProfitInvestScreen;
    public GameObject eZProfitAmountInputScreen;

    [Header("EZProfit Credit Card")]
    public GameObject eZProfitAddPaymentMethodScreen;
    public GameObject eZProfitCreditCardDetailsScreen;
    public GameObject eZProfitScanningCreditCardScreen;
    public GameObject eZProfitCreditCardDetailsAddedScreen;
    public GameObject eZProfitExistingCardScreen;
    public GameObject eZProfitLoadingCardScreen;
    public GameObject eZProfitInvestmentSuccessScreen;
    public GameObject eZProfitEarningsScreen;

    [Header("EZProfit Withdraw")]
    public GameObject eZProfitWithdrawLoadingScreen;
    public GameObject eZProfitWithdrawDeniedScreen;

    public float loadingTime;

    public void ScammerNotification()
    {
        kachagramScreen.SetActive(true);
        StartCoroutine(scamManager.WaitAndContinueStory(0));
    }

    public void AddProfilePicture(int index)
    {
        rachelKachagramPfp.transform.SetParent(scenarioController.messageContentParent);
        rachelKachagramPfp.transform.SetAsFirstSibling();
        string selectedText = scamManager.playerChoices[index].choiceName;
        scamManager.messagingSystem.PlayerNextMessage(selectedText);
        StartCoroutine(scamManager.WaitAndContinueStory(scamManager.messageTime));
    }

    public void HomeScreen()
    {
        scamManager.isOnHomeScreen = true;
        DisableAllCanvasChildren();
        homeScreen.SetActive(true);

        if (scamManager.scamshieldButton != null)
        {
            Destroy(scamManager.scamshieldButton);
        }
        StartCoroutine(scamManager.WaitAndContinueStory(1));
    }

    public void OpenAppShop()
    {
        scamManager.isOnHomeScreen = false;
        appShopScreen.SetActive(true);
        StartCoroutine(scamManager.WaitAndContinueStory(1));
    }

    public void OpenEZProfitInAppShop()
    {
        scamManager.isOnHomeScreen = false;
        eZProfitAppScreen.SetActive(true);
        StartCoroutine(scamManager.WaitAndContinueStory(1));
    }
    public IEnumerator DownloadingEZProfit()
    {
        eZProfitAppDownloadingScreen.SetActive(true);
        yield return new WaitForSeconds(loadingTime);
        StartCoroutine(scamManager.WaitAndContinueStory(0));
        eZProfitAppDownloadedScreen.SetActive(true);
        eZProfitAppIcon.SetActive(true);
    }

    public void OpenEZProfitApp()
    {
        if (!scamManager.eZProfitAccountCreated)
        {
            eZProfitSignUpScreen.SetActive(true);
        }
        else
        {
            eZProfitHomeScreen.SetActive(true);
        }
        StartCoroutine(scamManager.WaitAndContinueStory(scamManager.messageTime));
    }

    public void FillUpAccountDetails()
    {
        eZProfitAccountDetailsScreen.SetActive(true);
        StartCoroutine(scamManager.WaitAndContinueStory(scamManager.messageTime));
    }

    public void SignUpEZProfitAccount()
    {
        eZProfitHomeScreen.SetActive(true);
        scamManager.eZProfitAccountCreated = true;
        StartCoroutine(scamManager.WaitAndContinueStory(scamManager.messageTime));
    }
    
    public void OpenKachagram()
    {
        scamManager.isOnHomeScreen = false;
        kachagramScreen.SetActive(true);
        StartCoroutine(scamManager.WaitAndContinueStory(1));
    }
    public void TapOnBaoBei()
    {
        eZProfitBaoBeiScreen.SetActive(true);
        StartCoroutine(scamManager.WaitAndContinueStory(scamManager.messageTime));
    }

    public void InvestInBaoBei()
    {
        if (!scamManager.isInvestAmountEntered)
        {
            eZProfitInvestScreen.SetActive(true);
        }
        else
        {
            eZProfitAddPaymentMethodScreen.SetActive(true);
        }
        StartCoroutine(scamManager.WaitAndContinueStory(inkManager.messageTime));
    }

    public void InvestAmount()
    {
        eZProfitAmountInputScreen.SetActive(true);
        scamManager.isInvestAmountEntered = true;
        StartCoroutine(scamManager.WaitAndContinueStory(inkManager.messageTime));
    }

    public void AddNewPaymentMethod()
    {
        eZProfitCreditCardDetailsScreen.SetActive(true);
        StartCoroutine(scamManager.WaitAndContinueStory(inkManager.messageTime));
    }

    public IEnumerator ScanCreditCard()
    {
        eZProfitScanningCreditCardScreen.SetActive(true);
        yield return new WaitForSeconds(loadingTime);
        eZProfitCreditCardDetailsAddedScreen.SetActive(true);
        StartCoroutine(scamManager.WaitAndContinueStory(0));
    }

    public void AddCreditCard()
    {
        eZProfitExistingCardScreen.SetActive(true);
        StartCoroutine(scamManager.WaitAndContinueStory(inkManager.messageTime));
    }

    public IEnumerator PickCard()
    {
        eZProfitLoadingCardScreen.SetActive(true);
        yield return new WaitForSeconds(loadingTime);
        eZProfitInvestmentSuccessScreen.SetActive(true);
        scamManager.isMoneyInvested = true;
        StartCoroutine(scamManager.WaitAndContinueStory(inkManager.messageTime));
    }

    public void EarningsScreen()
    {
        eZProfitEarningsScreen.SetActive(true);
        StartCoroutine(scamManager.WaitAndContinueStory(inkManager.messageTime));
    }

    public IEnumerator WithdrawEarnings()
    {
        eZProfitWithdrawLoadingScreen.SetActive(true);
        yield return new WaitForSeconds(loadingTime);
        eZProfitWithdrawDeniedScreen.SetActive(true);
        StartCoroutine(scamManager.WaitAndContinueStory(inkManager.messageTime));
    }

    public void AskRachel()
    {
        DisableAllCanvasChildren();
        kachagramScreen.SetActive(true);
        StartCoroutine(scamManager.WaitAndContinueStory(1));
    }
}
