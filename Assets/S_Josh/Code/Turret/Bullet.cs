using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    private Transform target;
    private int damage;

    public void SetTarget(Transform target, int damage)
    {
        this.target = target;
        this.damage = damage;
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject); // Destroy the bullet if the target is gone
            return;
        }

        // Move the bullet towards the target
        Vector3 direction = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (direction.magnitude <= distanceThisFrame)
        {
            // Bullet reached the target
            HitTarget();
            return;
        }

        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
    }

    void HitTarget()
    {
        // Apply damage to the target
        Target targetComponent = target.GetComponent<Target>();
        if (targetComponent != null)
        {
            targetComponent.TakeDamage(damage);
        }

        // Destroy the bullet after it hits the target
        Destroy(gameObject);
    }
}
