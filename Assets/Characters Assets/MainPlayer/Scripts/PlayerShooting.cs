using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    // Start is called before the first frame update
    public int damage = 20;
    public float range = 100f;
    AudioSource shooting_sound;

    
    public GameObject fpsCam;
    private Animator animator;

    void Start()
    {
        shooting_sound = GetComponent<AudioSource>();
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //RaycastHit hit;

        if (Input.GetButtonDown("Fire1"))
            Shoot();
    }

    void Shoot()
    {
        //animator.SetTrigger("Shooting");

        RaycastHit hit;
        shooting_sound.Play();
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit,range))
        {
            //Defender target = hit.transform.GetComponent<Defender>();
            Attacker target = hit.transform.GetComponent<Attacker>();
            if (target != null)
            {
                Debug.Log("yess");
                target.TakeDamage(damage);
            }
            else
                Debug.Log("NOOO");
        }
    }
}
