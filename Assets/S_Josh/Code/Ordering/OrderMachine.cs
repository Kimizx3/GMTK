using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderMachine : MonoBehaviour
{
    public float timeToOrderMultiplier = 1.0f;
    public bool IsAvailable = true;
    public Slider progressBar;


    private void Start() {
        progressBar.gameObject.SetActive(false);
    
    }
}
