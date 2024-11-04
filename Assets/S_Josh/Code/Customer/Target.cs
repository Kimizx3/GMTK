using System.Collections;
using System.Collections.Generic;
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
    public float timeToFill = 50f; // Time it takes to fill the progress bar
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
    public List<Vector3> WalkingPath = new List<Vector3>();
    public float MoveSpeed = 5f;
    private int walkingPathIndex = 1;
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
        HandleMovement();
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
        
        Vector3 startPosition = transform.localPosition;
        float elapsed = 0.0f;

        while (elapsed < shakeDuration)
        {
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;

            transform.localPosition = new Vector3(transform.localPosition.x + x, transform.localPosition.y + y, startPosition.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = startPosition; // Reset to the original position
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
        if(gameObject == null || other.gameObject == null)
        {
            return false;
        }
        return gameObject == other.gameObject;
    }
    public override int GetHashCode()
    {
        return gameObject.GetHashCode();
    }

    private void HandleMovement()
    {
        //asuming the target is already starting at walkingpath[0]
        
        if (WalkingPath != null && WalkingPath.Count > 0 && walkingPathIndex < WalkingPath.Count)
        {
            Vector3 targetPosition = WalkingPath[walkingPathIndex];
            if(Vector3.Distance(transform.position, targetPosition) > 0.1f)
            {
                Vector3 moveDir = (targetPosition - transform.position).normalized;
                transform.position += moveDir * MoveSpeed * Time.deltaTime;
            }
            else
            {
                walkingPathIndex++;
                if(walkingPathIndex >= WalkingPath.Count)
                {
                    WalkingPath = null;
                    walkingPathIndex = 1;
                }
            }
        }
    }
}
