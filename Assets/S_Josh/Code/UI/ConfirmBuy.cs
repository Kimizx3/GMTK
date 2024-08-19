using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmBuy : MonoBehaviour
{
    public BuyEvent buyEvent;
    public Button Confirm;
    public Button Cancel;
    public TMPro.TMP_Text Description;
    private void Awake() {
        Confirm.onClick.AddListener(ConfirmOnClick);
        Cancel.onClick.AddListener(CancelOnClick);
    }

    private void OnEnable() {
        Description.text = "Do you want to buy " + buyEvent.ItemName + " for " + buyEvent.Cost + " coins?";
    }

    public void ConfirmOnClick()
    {
        if(buyEvent == null)
        {
            Debug.LogError("Buy Event is null");
            return;
        }
        buyEvent.RaiseEvent();
        gameObject.SetActive(false);
    }
    public void CancelOnClick()
    {
        gameObject.SetActive(false);
    }

}
