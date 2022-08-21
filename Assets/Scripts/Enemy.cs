using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private Animator animator;
    public float health = 50f;
    public GameObject theDestinaion;
    NavMeshAgent theAgent;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        theAgent= GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.Z))
        //{
        //    animator.SetInteger("Status", 1);
        //}
        theAgent.SetDestination(theDestinaion.transform.position);

    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        animator.SetTrigger("ShotTrigger");
        if (health <= 0f)
            Die();
    }

    void Die()
    {
        animator.SetInteger("Status", 3);
        theAgent.isStopped = true;

    }

}
