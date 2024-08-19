using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyMaterial : MonoBehaviour
{
    public RawMaterial material;
    public IntVariable Money;
    Button BuyButton;
    


    private void Start() {
        BuyButton = GetComponent<Button>();
        BuyButton.onClick.AddListener(Buy);
        BuyButton.GetComponentInChildren<TMPro.TMP_Text>().text = $"{material.Cost} $-";
    }

    void Buy()
    {
        if (Money.Value >= material.Cost)
        {
            Money.Value -= material.Cost;
            material.AmountLeft += 1;
        }
    }
}
