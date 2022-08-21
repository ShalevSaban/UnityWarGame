using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowHit : MonoBehaviour
{
    public int damage = 25;
    private void Start()
    {
        Destroy(gameObject, 3);
    }
}
