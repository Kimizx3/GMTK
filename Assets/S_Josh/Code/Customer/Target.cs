using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Target : MonoBehaviour
{
    public VoidEventChannel OnDeathEvent;
    public int health = 100;
    public float shakeDuration = 0.1f; // Duration of the shake
    public float shakeMagnitude = 0.1f; // Magnitude of the shake
    public IntVariable PlayerHealth;
    public int damage = 5; 
    public float timeToFill = 10f; // Time it takes to fill the progress bar
    public Slider progressBar; // Reference to the UI Slider component for the progress bar
    private Coroutine loadProgressCoroutine;
    private bool isLoading = false;
    public SeatSpace CurrentSeat;
    public VoidEventChannel StartOrderEvent;
    public ListGameObjectVariable SeatedTargets;
    public float TimeToOrder = 5.0f;

    public int MoneyValue;
    public bool StartToGetMad = false;
    public GameObject Health;

    public VoidEventChannel CustomerLeft; 

    public Vector3 originalPosition;
    private void Start() {
        StartToGetMad = false;
        progressBar.gameObject.SetActive(false);
        StartLoading();
    }
    private void Update() {
        StartLoading();
        if(CurrentSeat == null)
        {
            Health.SetActive(false);
        }
        else
        {
            Health.SetActive(true);
        }
    }
    private void StartLoading()
    {
        if (!isLoading && StartToGetMad)
        {
            loadProgressCoroutine = StartCoroutine(LoadProgressBar());
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(Shake());
        }
    }

    private void Die()
    {
        // Handle target's death (e.g., play an animation, remove from the scene)
        //Debug.Log(gameObject.name + " has died.");
        SeatedTargets.listGameObject.Remove(gameObject);
        OnDeathEvent.RaiseEvent(); 
        CustomerLeft.RaiseEvent();
        CurrentSeat.currentTarget = null;
        StartOrderEvent.RaiseEvent();
        Destroy(gameObject);
    }
    private IEnumerator Shake()
    {
        

        float elapsed = 0.0f;

        while (elapsed < shakeDuration)
        {
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;

            transform.localPosition = new Vector3(originalPosition.x + x, originalPosition.y + y, originalPosition.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPosition; // Reset to the original position
    }

    private IEnumerator LoadProgressBar()
    {
        progressBar.gameObject.SetActive(true);
        isLoading = true;
        float elapsedTime = 0f;

        while (elapsedTime < timeToFill)
        {
            // If the target is destroyed during this time, exit the coroutine early
            if (this == null)
            {
                yield break;
            }

            elapsedTime += Time.deltaTime;
            progressBar.value = Mathf.Clamp01(elapsedTime / timeToFill);
            yield return null;
        }

        OnProgressBarFull();
    }

    private void OnProgressBarFull()
    {
        // Reduce player health when the progress bar is full
        PlayerHealth.Value -= damage;

        // Handle target's death
        Die();
    }


     public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        Target other = (Target)obj;
        return gameObject == other.gameObject;
    }
    public override int GetHashCode()
    {
        return gameObject.GetHashCode();
    }
}
