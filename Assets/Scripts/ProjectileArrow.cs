using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileArrow : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private int damage = 10;
    [SerializeField] private float lifetime = 5f;
    private string enemyTag;
    [SerializeField] private float upwardForce = 5f;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        Destroy(gameObject, lifetime);

        if (tag == "Red")
        {
            enemyTag = "Blue";
        }
        else if (tag == "Blue")
        {
            enemyTag = "Red";
        }
        
        Transform target = FindNearestEnemy();
        Vector3 direction = (target.position - transform.position).normalized;

        direction.y += upwardForce / speed;
        direction = direction.normalized;
        rb.velocity = direction * speed;
        
    }

    private Transform FindNearestEnemy()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 60f);
        Transform nearestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag(enemyTag))
            {
                float distance = Vector3.Distance(transform.position, collider.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    nearestEnemy = collider.transform;
                }
            }
        }

        return nearestEnemy;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("PLEASE HIT");
        if (other.CompareTag(enemyTag))
        {
            Health targetHealth = other.GetComponent<Health>();
            if (targetHealth != null)
            {
                targetHealth.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}
