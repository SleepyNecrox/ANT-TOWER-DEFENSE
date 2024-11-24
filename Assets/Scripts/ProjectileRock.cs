using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ProjectileRock : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int damage;
    [SerializeField] private float lifetime;

    private string enemyTag;

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
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("PLEASE HIT");
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