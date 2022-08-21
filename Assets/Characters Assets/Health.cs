using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int HP = 100;
    public GameController controller;
    public Slider slider;
    public GameObject bloodEffect;

    public int enemyTeamNumber;
    public int GetCurrentHP()
    {
        return HP;
    }
    public void TakeDamage(int damageAmount)
    {
        Vector3 bloodPosition = transform.position;
        bloodPosition.y += 1;
        Instantiate(bloodEffect, bloodPosition, Quaternion.identity);

        HP -= damageAmount;

        slider.value = HP;

        if (HP <= 0)
        {
            gameObject.tag = "Untagged";
            GetComponent<Collider>().enabled = false;
            controller.FinishGame(enemyTeamNumber);
        }
    }
}
