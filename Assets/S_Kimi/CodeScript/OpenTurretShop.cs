using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OpenTurretShop : MonoBehaviour
{
    [Header("Shop")]
    public GameObject shopPage;
    public Button closeBut;
    public Button openBut;
    private bool isOpened = false;

    private void Start()
    {
        if (closeBut != null)
        {
            closeBut.onClick.AddListener(CloseShop);
        }

        if (openBut != null)
        {
            openBut.onClick.AddListener(OpenShop);
        }

        if (shopPage != null)
        {
            shopPage.SetActive(false);
        }
    }

    private void CloseShop()
    {
        if (shopPage != null && isOpened)
        {
            shopPage.SetActive(false);
            isOpened = false;
        }
    }

    private void OpenShop()
    {
        if (shopPage !=null && !isOpened)
        {
            shopPage.SetActive(true);
            isOpened = true;
        }
    }
}
