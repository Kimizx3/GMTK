using System.Collections;
using UnityEngine;

public class Target : MonoBehaviour
{
    public VoidEventChannel OnDeathEvent;
    public int health = 100;
    public float shakeDuration = 0.1f; // Duration of the shake
    public float shakeMagnitude = 0.1f; // Magnitude of the shake
    Vector3 originalPosition;
    private void Start() {
        originalPosition = transform.localPosition;
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
        Debug.Log(gameObject.name + " has died.");
        OnDeathEvent.RaiseEvent();
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
}
