using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Slider healthSlider; // Reference to the UI Slider component
    public Target currentTarget; // Reference to the target's health
    private void Start()
    {
        healthSlider = GetComponent<Slider>(); // Get the Slider component on this GameObject
        // Initialize the slider's maximum value and current value
        healthSlider.maxValue = currentTarget.health;
        healthSlider.value = currentTarget.health;
    }

    private void Update()
    {
        // Update the slider's value to reflect the player's current health
        healthSlider.value = currentTarget.health;
    }
}
