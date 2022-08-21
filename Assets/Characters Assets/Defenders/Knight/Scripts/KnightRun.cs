using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class KnightRun : StateMachineBehaviour
{
    NavMeshAgent agent;
    GameObject target;
    Transform targetLocation;
    private float attackingDistance;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponent<NavMeshAgent>();
        attackingDistance = animator.GetComponentInParent<Defender>().attackingDistance;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        target = animator.GetComponentInParent<Defender>().FindClosestEnemy();

        if (!target)
        {
            animator.SetBool("isRunning", false);   // Game Over
            animator.SetBool("Idle", true);
        }
        else
        {
            targetLocation = target.transform;
            agent.SetDestination(targetLocation.position);
            animator.transform.LookAt(targetLocation);

            if (Vector3.Distance(targetLocation.position, animator.transform.position) < attackingDistance)
            {
                animator.SetBool("isRunning", false);
                animator.SetBool("isAttacking", true);
            }
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(animator.transform.position);
    }
}
