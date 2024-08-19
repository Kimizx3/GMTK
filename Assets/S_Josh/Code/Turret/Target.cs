using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public VoidEventChannel OnDeathEvent;
    public int health = 100;

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Handle target's death (e.g., play an animation, remove from the scene)
        Debug.Log(gameObject.name + " has died.");
        OnDeathEvent.RaiseEvent();
        Destroy(gameObject);
    }
}
