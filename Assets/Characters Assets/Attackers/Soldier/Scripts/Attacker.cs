using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    private Animator animator;

    public int HP = 100;
    public int aimDistance = 20;
    private GameObject currentTarget;

    public GameObject projectile;
    public float startTimeBetweenShots;
    private float timeBetweenShots;

    public GameObject bloodEffect;

    public TeamAttackers team;
    private AudioSource shootingSound;

    void Start()
    {
        animator = GetComponent<Animator>();
        timeBetweenShots = startTimeBetweenShots;
        shootingSound = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (HP > 0)
        {
            if (animator.GetBool("isAttacking"))
            {
                animator.transform.LookAt(currentTarget.transform);

                bool enemyAlive = GetEnemyIsAlive(currentTarget);
                bool enemyInAimDistance = Vector3.Distance(currentTarget.transform.position, animator.transform.position) < aimDistance;

                if (enemyAlive && enemyInAimDistance)
                    Shoot();
                else
                {
                    animator.SetBool("isAttacking", false);
                    animator.SetBool("isRunning", true);
                }
            }
        }
    }

    private void Shoot()
    {
        if (timeBetweenShots <= 0)
        {
            shootingSound.Play();
            GameObject attack = Instantiate(projectile, transform.position, Quaternion.identity);
            attack.GetComponent<Bullet>().SetTarget(currentTarget);
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
        return (enemy.name == "ArcherPlayer" && enemy.GetComponent<Health>().GetCurrentHP() > 0) ||
                    (enemy.name != "ArcherPlayer" && enemy.GetComponent<Defender>().GetCurrentHP() > 0);
    }


    public GameObject FindClosestEnemy()
    {
        GameObject[] defenders = GameObject.FindGameObjectsWithTag("DeffendPlayer");
        if (defenders.Length == 0)
            return null;

        GameObject closest = null;
        float distance = Mathf.Infinity;

        foreach (GameObject go in defenders)
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
