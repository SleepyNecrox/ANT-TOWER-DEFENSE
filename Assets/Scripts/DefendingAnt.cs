using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefendingAnt : MonoBehaviour
{
    public enum TurretState
    {
        Idle,
        Targeting,
        Firing,
    }

    [SerializeField] internal float range;
    [SerializeField] internal float fireRate;
    [SerializeField] internal LayerMask enemyLayer;
    [SerializeField] private Transform shootArea;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private TurretState currentState;
    private float fireTimer;
    private Transform target;

    private Animator animator;
    void Start()
    {
        animator = GetComponentInParent<Animator>();
        currentState = TurretState.Idle;
        fireTimer = fireRate;
    }
     void Update()
    {
        switch (currentState)
        {
            case TurretState.Idle:
                SearchForTarget();
                break;

            case TurretState.Targeting:
                AimAtTarget();
                break;

            case TurretState.Firing:
                FireAtTarget();
                break;
        }
    }

    private void SearchForTarget()
    {
        Collider[] detectedTargets = Physics.OverlapSphere(transform.position, range, enemyLayer);
        if (detectedTargets.Length > 0)
        {
            target = detectedTargets[0].transform;
            currentState = TurretState.Targeting;
        }
        else
        {
            animator.SetBool("Attack", false);
        }
    }

    private void AimAtTarget()
    {
        if (target == null || Vector3.Distance(transform.position, target.position) > range)
        {
            target = null;
            currentState = TurretState.Idle;
            return;
        }

        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

        if (fireTimer <= 0f)
        {
            currentState = TurretState.Firing;
        }
        else
        {
            fireTimer -= Time.deltaTime;
        }
    }

    private void FireAtTarget()
    {
        if (target == null)
        {
            currentState = TurretState.Idle;
            return;
        }

        animator.SetBool("Attack", true);
        Instantiate(projectilePrefab, shootArea.position, shootArea.rotation);

        fireTimer = fireRate;
        currentState = TurretState.Targeting;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
