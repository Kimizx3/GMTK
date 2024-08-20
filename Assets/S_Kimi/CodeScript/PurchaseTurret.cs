using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PurchaseTurret : MonoBehaviour
{
    MenuHolder menuHolder; // Reference to the MenuHolder script
    public Button PurchaseButton; // Button to trigger the purchase
    public GameObject[] relatedItems; // Related items that will be activated upon purchase
    public int[] optionIndices;

    private bool isPurchased = false;

    private void Start()
    {
        // Add a listener to the button to handle the purchase action
        PurchaseButton.onClick.AddListener(PurchaseItem);

        // Ensure all related items are inactive at the start
        if (relatedItems != null)
        {
            foreach (GameObject item in relatedItems)
            {
                if (item != null)
                {
                    item.SetActive(false);
                }
            }
        }
    }
    
    private void PurchaseItem()
    {
        // Check if the item has already been purchased
        if (!isPurchased)
        {
            isPurchased = true;

            // Disable the purchase button and change its color
            PurchaseButton.interactable = false;
            PurchaseButton.image.color = Color.gray;

            // Activate all related items
            if (relatedItems != null)
            {
                foreach (GameObject item in relatedItems)
                {
                    if (item != null)
                    {
                        item.SetActive(true);
                    }
                }
            }

            // Add each prefab in myNewPrefab array to the menu
            if (menuHolder != null && optionIndices != null)
            {
                foreach (int index in optionIndices)
                {
                    menuHolder.ActivateMenuOption(index);
                }
            }
            else
            {
                Debug.LogWarning("MenuHolder or myNewPrefab array is null.");
            }
        }
    }
}


