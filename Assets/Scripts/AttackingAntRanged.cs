using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;

public class AttackingAntRanged : MonoBehaviour
{
    private Transform enemyBase;
    [SerializeField] internal float detectionRadius;
    [SerializeField] internal float attackDistance;
    [SerializeField] internal float attackCooldown;
    [SerializeField] internal GameObject arrowPrefab;

    [SerializeField] internal Transform shootArea;


    private NavMeshAgent agent;
    private Transform currentTarget;
    private float attackTimer;
    private string enemyTag;

    private AntStats antStats;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        antStats = GetComponent<AntStats>();

        agent = GetComponent<NavMeshAgent>();
        agent.speed = antStats.speed;

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

        if (currentTarget != null)
        {
            float distanceToTarget = Vector3.Distance(transform.position, currentTarget.position);

            if (distanceToTarget <= attackDistance)
            {
                AttackTarget();
                agent.isStopped = true;
            }

            else
            {
                animator.SetBool("Attack", false);
                agent.isStopped = false;
                MoveToTarget();
            }
        }
    }

    private void MoveToTarget()
    {
        if (currentTarget != null)
        {
            animator.SetBool("Attack", false);
            agent.isStopped = false;
            agent.SetDestination(currentTarget.position);
        }
    }

    [PunRPC]
    private void AttackTarget()
    {
        if (attackTimer <= 0f)
        {
            animator.SetBool("Attack", true);
            agent.isStopped = true;

            Instantiate(arrowPrefab, shootArea.position, shootArea.rotation);
            
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
