using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defender : MonoBehaviour
{
    private Animator animator;

    public int HP = 100;
    public float attackingDistance = 1.5F;
    private GameObject currentTarget;

    public GameObject projectile;
    public float startTimeBetweenShots;
    private float timeBetweenShots;
    private AudioSource slashing;
    public TeamDefenders team;

    public GameObject bloodEffect;

    void Start()
    {
        animator = GetComponent<Animator>();
        slashing = GetComponent<AudioSource>();
        timeBetweenShots = startTimeBetweenShots;
    }

    void Update()
    {
        if (HP > 0)
        {
            if (animator.GetBool("isAttacking"))
            {

                bool enemyAlive = GetEnemyIsAlive(currentTarget);
                bool enemyInAimDistance = Vector3.Distance(currentTarget.transform.position, animator.transform.position) < attackingDistance;

                if (enemyAlive && enemyInAimDistance)
                    Attack();
                else
                {
                    animator.SetBool("isAttacking", false);
                    animator.SetBool("isRunning", true);
                }
            }
        }
    }
    private void Attack()
    {
        if (timeBetweenShots <= 0)
        {
            slashing.Play();
            GameObject attack = Instantiate(projectile, transform.position, Quaternion.identity);
            attack.GetComponent<SwordAttack>().SetTarget(currentTarget);
            //Instantiate(projectile, transform.position, Quaternion.identity);
            timeBetweenShots = startTimeBetweenShots;
        }
        else
            timeBetweenShots -= Time.deltaTime;
    }

    public void SetTarget(GameObject t)
    {
        currentTarget = t;
    }

    public GameObject GetTarget()
    {
        return currentTarget;
    }

    public int GetCurrentHP()
    {
        return HP;
    }

    private bool GetEnemyIsAlive(GameObject enemy)
    {
        return (enemy.name == "PlayerSoldier" && enemy.GetComponent<Health>().GetCurrentHP() > 0) ||
                    (enemy.name != "PlayerSoldier" && enemy.GetComponent<Attacker>().GetCurrentHP() > 0);
    }

    public GameObject FindClosestEnemy()
    {
        GameObject[] attackers = GameObject.FindGameObjectsWithTag("AttackPlayer");

        if (attackers.Length == 0)
            return null;

        GameObject closest = null;
        float distance = Mathf.Infinity;

        foreach (GameObject go in attackers)
        {
            if (GetEnemyIsAlive(go))
            {
                Vector3 diff = go.transform.position - transform.position;
                float curDistance = diff.sqrMagnitude;
                if (curDistance < distance)
                {
                    closest = go;
                    distance = curDistance;
                }
            }
        }

        SetTarget(closest);
        return closest;
    }

    public void TakeDamage(int damageAmount)
    {
        Vector3 bloodPosition = transform.position;
        bloodPosition.y += 1;
        Instantiate(bloodEffect, bloodPosition, Quaternion.identity);

        HP -= damageAmount;

        if (HP <= 0)
        {
            gameObject.tag = "Untagged";
            animator.SetTrigger("Die");
            animator.SetBool("isAlive", false);
            GetComponent<Collider>().enabled = false;
            team.decrementAliveCount();
        }
        else
        {
            animator.SetTrigger("damage");
        }
    }
}
