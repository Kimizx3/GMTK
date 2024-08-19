using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public ConfirmBuyEvent confirmBuyEvent;
    public GameObject BuyPanel;

    void OnEnable() {
        confirmBuyEvent.OnEventRaised += ShowBuyPanel;
    }

    void OnDisable() {
        confirmBuyEvent.OnEventRaised -= ShowBuyPanel;
    }   


    void ShowBuyPanel(BuyEvent buyEvent) {
       
        BuyPanel.GetComponent<ConfirmBuy>().buyEvent = buyEvent;
        BuyPanel.SetActive(true);
    }

}
