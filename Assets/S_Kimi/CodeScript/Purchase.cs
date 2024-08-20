using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Purchase : MonoBehaviour
{
    MenuHolder menuHolder;
    public Button[] PurchaseButton;
    public Button CancelButton;
    public GameObject[] relatedItem;
    private bool isPurchased;
    private int currentTurretIndex = -1;

    public GameObject[] currentTurret;
    
    public ListGameObjectVariable placedTurrets;
    public VoidEventChannel TurretPlaced;

    private void Start()
    {
        // Set up listeners for each purchase button
        for (int i = 0; i < PurchaseButton.Length; i++)
        {
            int index = i;
            PurchaseButton[i].onClick.AddListener(() => PurchaseItem(index));
        }

        // Set up the cancel button listener
        if (CancelButton != null)
        {
            CancelButton.onClick.AddListener(CancelPurchase);
            CancelButton.interactable = false; // Disable cancel button initially
        }

        // Initially disable all related items
        if (relatedItem != null)
        {
            foreach (GameObject item in relatedItem)
            {
                if (item != null)
                {
                    item.SetActive(false);
                }
            }
        }
    }

    public void PurchaseItem(int index)
    {
        if (!isPurchased && currentTurret[index] != null)
        {
            isPurchased = true;
            currentTurretIndex = index;

            foreach (Button button in PurchaseButton)
            {
                button.interactable = false;
            }
            
            // Disable the purchase button and change its color
            PurchaseButton[index].interactable = false;
            PurchaseButton[index].image.color = Color.gray;

            // Enable the cancel button
            if (CancelButton != null)
            {
                CancelButton.interactable = true;
            }

            // Activate related items
            if (relatedItem != null)
            {
                foreach (GameObject item in relatedItem)
                {
                    if (item != null)
                    {
                        item.SetActive(true);
                    }
                }
            }

            // Add the current turret to the placed turrets list
            placedTurrets.listGameObject.Add(currentTurret[index]);

            // Trigger a UI redraw if necessary
            if (TurretPlaced != null)
            {
                TurretPlaced.RaiseEvent();
            }
        }
    }

    public void CancelPurchase()
    {
        if (isPurchased && currentTurretIndex != -1)
        {
            isPurchased = false;

            foreach (Button button in PurchaseButton)
            {
                button.interactable = true;
            }

            PurchaseButton[currentTurretIndex].interactable = true;
            PurchaseButton[currentTurretIndex].image.color = Color.white;

            if (relatedItem != null)
            {
                foreach (GameObject item in relatedItem)
                {
                    if (item != null)
                    {
                        item.SetActive(false);
                    }
                }
            }

            placedTurrets.listGameObject.Remove(currentTurret[currentTurretIndex]);

            if (CancelButton != null)
            {
                CancelButton.interactable = false;
            }

            currentTurretIndex = -1;
        }
    }
}

