using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Purchase : MonoBehaviour
{
    public Button PruchaseButton;
    public GameObject relatedItem;

    private bool isPurchased = false;

    private void Start()
    {
        PruchaseButton.onClick.AddListener(PurchaseItem);
    }

    private void PurchaseItem()
    {
        if (!isPurchased)
        {
            isPurchased = true;
            PruchaseButton.interactable = false;
            PruchaseButton.image.color = Color.gray;
            if (relatedItem != null)
            {
                relatedItem.SetActive(true);
            }
        }
        
    }
}
