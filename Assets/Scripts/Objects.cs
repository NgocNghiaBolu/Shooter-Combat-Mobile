using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objects : MonoBehaviour
{
    public float healthObject = 100;

    public void TakeDamage(float damage)
    {
        healthObject -= damage;

        if(healthObject <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
