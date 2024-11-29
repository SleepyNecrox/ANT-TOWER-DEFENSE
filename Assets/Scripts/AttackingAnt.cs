using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;

public class AttackingAnt : MonoBehaviour
{
    private Transform enemyBase;
    [SerializeField] internal float detectionRadius;
    [SerializeField] internal float attackDistance;
    [SerializeField] internal float attackCooldown;
    private NavMeshAgent agent;
    private Transform currentTarget;
    private float attackTimer;
    private string enemyTag;
    private AntStats antStats;
    private Animator animator;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindWithTag("Audio").GetComponent<AudioManager>();
    }

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

        if (currentTarget != null && Vector3.Distance(transform.position, currentTarget.position) <= attackDistance)
        {
            AttackTarget();
        }

        else
        {
            animator.SetBool("Attack", false);
            agent.isStopped = false;
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
            audioManager.PlaySFX(audioManager.Hit);
            animator.SetBool("Attack", true);
            agent.isStopped = true;

            Health targetHealth = currentTarget.GetComponent<Health>();
            if (targetHealth != null)
            {
                targetHealth.TakeDamage(antStats.damage);
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
