using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    GameObject target;
    Vector3 targetPosition;
    public float speed;
    public int damage = 25;
    private void Start()
    {
        if (target)
            targetPosition = target.transform.position;
        else
            targetPosition = FindClosestEnemy().transform.position;

        Destroy(gameObject, 2);
    }
    private void Update()
    {
        targetPosition = target.transform.position;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "PlayerSoldier" && other.GetComponent<Health>().GetCurrentHP() > 0)
            other.GetComponent<Health>().TakeDamage(damage);

        else if (other.tag == "AttackPlayer" && other.GetComponent<Attacker>().GetCurrentHP() > 0)
            other.GetComponent<Attacker>().TakeDamage(damage);

        Destroy(gameObject, 1);
    }

    public void SetTarget(GameObject t)
    {
        target = t;
    }

    private GameObject FindClosestEnemy()
    {
        GameObject[] attackers = GameObject.FindGameObjectsWithTag("AttackPlayer");

        if (attackers.Length == 0)
            return null;

        GameObject closest = null;
        float distance = Mathf.Infinity;

        foreach (GameObject go in attackers)
        {
            if ((go.name == "PlayerSoldier" && go.GetComponent<Health>().GetCurrentHP() > 0) ||
                (go.name != "PlayerSoldier" && go.GetComponent<Attacker>().GetCurrentHP() > 0))
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

        return closest;
    }
}
