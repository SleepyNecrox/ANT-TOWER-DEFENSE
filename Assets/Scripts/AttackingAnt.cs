using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackingAnt : MonoBehaviour
{
    private Transform enemyBase;
    [SerializeField] internal float detectionRadius = 5f;
    [SerializeField] internal float attackDistance = 1.5f;
    [SerializeField] internal float moveSpeed = 3.5f;
    [SerializeField] internal float attackCooldown = 1.5f;
    [SerializeField] internal int attackDamage = 10;

    private NavMeshAgent agent;
    private Transform currentTarget;
    private bool isAttacking;
    private float attackTimer;

    private string enemyTag;

    void Start()
{
    agent = GetComponent<NavMeshAgent>();
    agent.speed = moveSpeed;

    if (tag == "Red")
    {
        enemyTag = "Blue";
        enemyBase = GameObject.FindWithTag("BlueBase").transform;
    }
    else if (tag == "Blue")
    {
        enemyTag = "Red";
        enemyBase = GameObject.FindWithTag("RedBase").transform;
    }

    currentTarget = enemyBase;
    MoveToTarget();
}

    void Update()
    {
        Collider[] enemiesInRange = Physics.OverlapSphere(transform.position, detectionRadius);

        Transform closestEnemy = GetClosestEnemy(enemiesInRange);

        if (closestEnemy != null)
        {
            if (closestEnemy != currentTarget)
            {
                currentTarget = closestEnemy;
                MoveToTarget();
            }
        }
        else if (currentTarget != enemyBase)
        {
            currentTarget = enemyBase;
            MoveToTarget();
        }

        if (currentTarget != null && Vector3.Distance(transform.position, currentTarget.position) <= attackDistance)
        {
            AttackTarget();
        }
        else
        {
            isAttacking = false;
            agent.isStopped = false;
        }
    }

    private void MoveToTarget()
    {
        if (currentTarget != null)
        {
            agent.isStopped = false;
            agent.SetDestination(currentTarget.position);
        }
    }

    private void AttackTarget()
    {
        if (attackTimer <= 0f)
        {
            agent.isStopped = true;

            Health targetHealth = currentTarget.GetComponent<Health>();
            if (targetHealth != null)
            {
                targetHealth.TakeDamage(attackDamage);
            }

            attackTimer = attackCooldown;
        }

        attackTimer -= Time.deltaTime;
    }

    private Transform GetClosestEnemy(Collider[] enemies)
    {
        Transform closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (Collider enemy in enemies)
        {
            if (enemy.CompareTag(enemyTag))
            {
                float distance = Vector3.Distance(transform.position, enemy.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEnemy = enemy.transform;
                }
            }
        }

        return closestEnemy;
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
