using UnityEngine;
using UnityEngine.UI;

public class PurchaseTurret : MonoBehaviour
{
    public Button PurchaseButton; // Corrected the typo in the variable name
    public GameObject[] relatedItems; // Updated to plural to indicate multiple items

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
        }
    }
}

