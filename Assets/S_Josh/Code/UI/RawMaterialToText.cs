using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RawMaterialToText : MonoBehaviour
{
    public RawMaterial materialVariable;
    private TMPro.TMP_Text textMeshPro;
    private int lastValue = 0;

    void Start()
    {
        textMeshPro = GetComponent<TMPro.TMP_Text>();
        UpdateText();  // Initial update to reflect the current value
    }

    void Update()
    {
        if (materialVariable != null && materialVariable.AmountLeft != lastValue)
        {
            UpdateText();
            lastValue = materialVariable.AmountLeft;  // Update the last known value
        }
    }

    void UpdateText()
    {
        if (textMeshPro != null)
        {
            textMeshPro.text = materialVariable.Name + "  Cost:" + materialVariable.Cost.ToString() + "  Amount Left:"+ materialVariable.AmountLeft.ToString();
        }
    }
}
