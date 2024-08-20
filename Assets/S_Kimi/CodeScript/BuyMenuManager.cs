using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyMenuManager : MonoBehaviour
{
   public ListGameObjectVariable PurchasedTurrets;
   public GameObject CancelButton;
   public bool HasBought = false;

    public VoidEventChannel RedrawMenu;
    public VoidEventChannel UpdateList;


    private void OnEnable() {
        PurchasedTurrets.listGameObject.Clear();
        UpdateList.OnEventRaised += UpdateCancelToMoveEnd;
    }


    private void OnDisable() {
        PurchasedTurrets.listGameObject.Clear();
        UpdateList.OnEventRaised -= UpdateCancelToMoveEnd;
    }


    private void UpdateCancelToMoveEnd()
    {   if(!HasBought)
        {
            PurchasedTurrets.listGameObject.Add(CancelButton);
            HasBought = true;
            
        }
        else
        {
            int index = PurchasedTurrets.listGameObject.IndexOf(CancelButton);
            if (index >= 0)
            {
                // Remove the item from its current position
                PurchasedTurrets.listGameObject.RemoveAt(index);

                // Add the item to the end of the list
                PurchasedTurrets.listGameObject.Add(CancelButton);
            }
        }
        RedrawMenu.RaiseEvent();
        
        
    }




}
