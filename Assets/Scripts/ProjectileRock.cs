using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ProjectileRock : MonoBehaviour
{

    public enum ProjectileType
    {
        Rock,
        Arrow
    }

    [SerializeField] private ProjectileType projectileType;
    [SerializeField] private float speed;
    [SerializeField] private int damage;
    [SerializeField] private float lifetime;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        Destroy(gameObject, lifetime);

        if(projectileType == ProjectileType.Arrow)
        {
            Vector3 direction = new Vector3(1, 1, 0).normalized;
            rb.AddForce(direction * speed, ForceMode.Impulse);
        }
    }

    void Update()
    {
        if(projectileType == ProjectileType.Rock)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("PLEASE HIT");
        Health targetHealth = other.GetComponent<Health>();
        if (targetHealth != null)
        {
            targetHealth.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
