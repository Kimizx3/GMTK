using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntVariableToText : MonoBehaviour
{
   public IntVariable intVariable;
    private TMPro.TMP_Text textMeshPro;
    public string prefix = "";

    private int lastValue = 0;

    void Start()
    {
        textMeshPro = GetComponent<TMPro.TMP_Text>();
        UpdateText();  // Initial update to reflect the current value
    }

    void Update()
    {
        if (intVariable != null && intVariable.Value != lastValue)
        {
            UpdateText();
            lastValue = intVariable.Value;  // Update the last known value
        }
    }

    void UpdateText()
    {
        if (textMeshPro != null)
        {
            textMeshPro.text = prefix + intVariable.Value.ToString();
        }
    }
}
