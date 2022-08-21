using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowShoot : MonoBehaviour
{
    public float Range;
    public GameObject Arrow;
    public int damage = 25;

    //public GameObject arrow;
    //public Transform arrowPoint;
    public GameObject Cam;

    public void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(Cam.transform.position, Cam.transform.forward, out hit, Range))
        {
            Instantiate(Arrow, hit.point, transform.rotation);

            if (hit.transform.tag == "AttackPlayer" && hit.transform.GetComponent<Attacker>().GetCurrentHP() > 0)
            {
                hit.transform.GetComponent<Attacker>().TakeDamage(damage);
            }
        }

        //Rigidbody rb = Instantiate(arrow, arrowPoint.position, transform.rotation).GetComponent<Rigidbody>();
        //rb.AddForce(transform.forward * 40f, ForceMode.Impulse);
    }
}
