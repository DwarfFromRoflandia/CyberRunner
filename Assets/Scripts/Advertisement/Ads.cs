using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using System;
public class Ads : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener, IUnityAdsInitializationListener
{
    public bool TestMode;

    public string AndroidId;

    void Start()
    {
        Advertisement.Initialize(AndroidId, TestMode, this);


    }
    public void ShowAd()
    {
        Advertisement.Show("Rewarded_Android", this);

    }
    public void OnInitializationComplete()
    {
        Debug.Log("������������� ������ �������");
        Advertisement.Load("Rewarded_Android", this);
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log(" ������ �������������");
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        Debug.Log("������� �����������");
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.Log("������� �� �����������");

        Debug.Log(error.ToString() + " " + message);

    }

    public void OnUnityAdsShowClick(string placementId)
    {
        ///
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        Advertisement.Load("Rewarded_Android", this);

        //����� ����� ������� �� ��������
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {

    }

    public void OnUnityAdsShowStart(string placementId)
    {
        print("������� ��������");
    }

    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {

    }
}
