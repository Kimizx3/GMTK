using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Optionlist : MonoBehaviour
{
   private bool isPurchased = false;
   public List<GameObject> relatedItems;
   public Button purchaseButton;
   public Color purchasedColor = Color.gray;

   private void Start()
   {
      purchaseButton.onClick.AddListener(PurchaseItem);
      isPurchased = false;
      SetRelatedItemsActive(false);
   }

   public void PurchaseItem()
   {
      // Check if the item is already purchased
      if (!isPurchased)
      {
         isPurchased = true;
         purchaseButton.image.color = purchasedColor;
         purchaseButton.interactable = false;
         SetRelatedItemsActive(true);
      }
   }

   private void SetRelatedItemsActive(bool isActive)
   {
      foreach (GameObject item in relatedItems)
      {
         if (item != null)
         {
            item.SetActive(isActive);
         }
      }
   }
   
}
