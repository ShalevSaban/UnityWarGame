using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    GameObject target;
    Vector3 targetPosition;
    public float speed;
    public int damage = 34;
    private void Start()
    {
        if (target)
            targetPosition = target.transform.position;
        else
            targetPosition = FindClosestEnemy().transform.position;
        //Destroy(gameObject, 5);
    }
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "ArcherPlayer" && other.GetComponent<Health>().GetCurrentHP() > 0)
            other.GetComponent<Health>().TakeDamage(damage);

        else if (other.tag == "DeffendPlayer" && other.GetComponent<Defender>().GetCurrentHP() > 0)
            other.GetComponent<Defender>().TakeDamage(damage);

        Destroy(gameObject, 1);
    }

    public void SetTarget(GameObject t)
    {
        target = t;
    }

    private GameObject FindClosestEnemy()
    {
        GameObject[] defenders = GameObject.FindGameObjectsWithTag("DeffendPlayer");
        if (defenders.Length == 0)
            return null;

        GameObject closest = null;
        float distance = Mathf.Infinity;

        foreach (GameObject go in defenders)
        {
            if ((go.name == "ArcherPlayer" && go.GetComponent<Health>().GetCurrentHP() > 0) ||
                (go.name != "ArcherPlayer" && go.GetComponent<Defender>().GetCurrentHP() > 0))
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
