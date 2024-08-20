using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Purchase : MonoBehaviour
{
    MenuHolder menuHolder;
    public Button PurchaseButton;
    public Button CancelButton;
    public GameObject relatedItem;

    private static Purchase currentPurchasedScript = null;
    private bool isPurchased = false;

    private void Start()
    {
        PurchaseButton.onClick.AddListener(PurchaseItem);

        if (CancelButton != null && CancelButton.onClick.GetPersistentEventCount() == 0)
        {
            CancelButton.onClick.AddListener(RemoveCurrentPurchase);
        }
        
        if (relatedItem != null)
        {
            relatedItem.SetActive(false);
        }
    }

    public void PurchaseItem()
    {
        if (!isPurchased)
        {
            if (currentPurchasedScript != null)
            {
                return;
            }
            
            isPurchased = true;
            currentPurchasedScript = this;
            
            PurchaseButton.interactable = false;
            PurchaseButton.image.color = Color.gray;
            
            if (relatedItem != null)
            {
                relatedItem.SetActive(true);
            }
        }
    }

    public void RemoveCurrentPurchase()
    {
        if (currentPurchasedScript != null)
        {
            currentPurchasedScript.RemovePurchase();
        }
    }

    public void RemovePurchase()
    {
        if (isPurchased)
        {
            if (relatedItem != null)
            {
                relatedItem.SetActive(false);
            }
            
            isPurchased = false;
            PurchaseButton.interactable = true;
            PurchaseButton.image.color = Color.white;

            if (currentPurchasedScript == this)
            {
                currentPurchasedScript = null;
            }
        }
    }
}
