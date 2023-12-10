using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class MAXInit : MonoBehaviour
{
    public UIScript uIScript;
    
    void Start()
    {
        MaxSdkCallbacks.OnSdkInitializedEvent += (MaxSdkBase.SdkConfiguration sdkConfiguration) => {
            //AppLovin SDK is initialized, start loading ads
            //LoadInterstitialAd();
            //LoadRewardedAd();
        };

        MaxSdk.SetSdkKey("");
        MaxSdk.InitializeSdk();

        // Register rewarded ad callbacks
        MaxSdkCallbacks.OnRewardedAdLoadedEvent += OnRewardedAdLoaded;
        //  MaxSdkCallbacks.OnRewardedAdFailedToDisplayEvent += OnRewardedAdFailedToDisplay;
        MaxSdkCallbacks.OnRewardedAdDisplayedEvent += OnRewardedAdDisplayed;
        MaxSdkCallbacks.OnRewardedAdClickedEvent += OnRewardedAdClicked;
        MaxSdkCallbacks.OnRewardedAdHiddenEvent += OnRewardedAdHidden;
        MaxSdkCallbacks.OnRewardedAdReceivedRewardEvent += OnRewardedAdReceivedReward;
        //MaxSdkCallbacks.OnInterstitialAdFailedToDisplayEvent
        MaxSdkCallbacks.OnInterstitialHiddenEvent += OnInterstitialHiddenEvent;

        LoadInterstitialAd();
        LoadRewardedAd();
    }

    // Function to load an interstitial ad
    private void LoadInterstitialAd()
    {
        MaxSdk.LoadInterstitial("");
    }

    // Function to load a rewarded ad
    private void LoadRewardedAd()
    {
        MaxSdk.LoadRewardedAd("");
    }

    // Function to show an interstitial ad
    public void ShowInterstitialAd()
    {
        //uIScript.reclame = false;
        if (uIScript.reclame)
        {
            if (MaxSdk.IsInterstitialReady(""))
            {
                MaxSdk.ShowInterstitial("");
                //GetWheelReclame(false);
                LoadInterstitialAd();
            }
            else
            {
                Debug.LogWarning("Interstitial ad is not ready. Make sure to load it before showing.");
                GetWheelReclame(false);
                LoadInterstitialAd(); // Load a new interstitial ad
            }
        }
        else
        {
            GetWheelReclame(false);
            //uIScript.reclame = true;
        }
        //LoadInterstitialAd(); // Load a new interstitial ad
    }

    // Function to show a rewarded ad
    public void ShowRewardedAd(bool isRewarded)
    {
        if (uIScript.reclame)
        {
            if (MaxSdk.IsRewardedAdReady(""))
            {
                GetWheelReclame(isRewarded);
                MaxSdk.ShowRewardedAd("");
            }
            else
            {
                Debug.LogWarning("Rewarded ad is not ready. Make sure to load it before showing.");
                LoadRewardedAd(); // Load a new rewarded ad
            }
        }
        else
        {
            GetWheelReclame(isRewarded);
        }
    }

    private void GetWheelReclame(bool Rewarded)
    {
        if (uIScript.wheelActive)
        {
            if (Rewarded)
            {
                uIScript.StopWheel();
            }
            else
            {
                uIScript.StopWithoutPrize();
            }
        }
    }

    private void GetButtonReclame(bool isMelee)
    {
        if (isMelee)
        {
            uIScript.slimeSpawner.SpawnMeleeSlime(true);
            uIScript.slimeSpawner.SpawnMeleeSlime(true);
        }
        else
        {
            uIScript.slimeSpawner.SpawnRangeSlime(true);
            uIScript.slimeSpawner.SpawnRangeSlime(true);
        }
    }

    // Rewarded ad loaded callback
    private void OnRewardedAdLoaded(string adUnitId)
    {
        Debug.Log("Rewarded ad loaded: " + adUnitId);
    }

    // Rewarded ad failed to display callback
    private void OnRewardedAdFailedToDisplay(string adUnitId, MaxSdkBase.ErrorInfo errorInfo)
    {
        Debug.Log("Rewarded ad failed to display: " + adUnitId + ", Error: " + errorInfo.Message);
    }

    // Rewarded ad displayed callback
    private void OnRewardedAdDisplayed(string adUnitId)
    {
        Debug.Log("Rewarded ad displayed: " + adUnitId);
    }

    // Rewarded ad clicked callback
    private void OnRewardedAdClicked(string adUnitId)
    {
        //uIScript.StopWheel();
        Debug.Log("Rewarded ad clicked: " + adUnitId);
    }

    // Rewarded ad hidden callback
    private void OnRewardedAdHidden(string adUnitId)
    {
        Debug.Log("Rewarded ad hidden: " + adUnitId);
        
        if (uIScript.wheelObject.activeSelf)
        {
            StartCoroutine(uIScript.StopRotateWheel());
        }
        else
        {
            GetButtonReclame(uIScript.buyMeleeSwitch);
        }

        LoadRewardedAd();
    }

    // Rewarded ad received reward callback
    private void OnRewardedAdReceivedReward(string adUnitId, MaxSdk.Reward reward)
    {
        string rewardName = reward.Label;
        int rewardAmount = reward.Amount;

        Debug.Log("Rewarded ad received reward: " + adUnitId + ", Reward name: " + rewardName + ", Reward amount: " + rewardAmount);

        //uIScript.StopWheel();
        // TODO: Implement the logic to reward the player based on the received reward
    }

    private void OnInterstitialHiddenEvent(string adUnitId)
    {
        uIScript.StopWithoutPrize();
        LoadInterstitialAd();
    }

}
