using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHealthBar : MonoBehaviour
{
    Slider healthSlider; // Reference to the UI Slider component
    public IntVariable playerHealth; // Reference to the player's health (an IntVariable)

    private void Start()
    {
        healthSlider = GetComponent<Slider>(); // Get the Slider component on this GameObject
        // Initialize the slider's maximum value and current value
        healthSlider.maxValue = playerHealth.Value;
        healthSlider.value = playerHealth.Value;
    }

    private void Update()
    {
        // Update the slider's value to reflect the player's current health
        healthSlider.value = playerHealth.Value;
    }
}
