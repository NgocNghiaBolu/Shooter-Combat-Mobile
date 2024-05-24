using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Player Health and Damage")]
    public float playerHealth = 100f;
    public float presentHealth;
    public HealthBar healthBar;

    private void Start()
    {
        presentHealth = playerHealth;
        healthBar.FullHealth(playerHealth);
    }

    public void PlayerHitDam(float damage)
    {
        presentHealth -= damage;
        healthBar.SetHealth(presentHealth);

        if(presentHealth <= 0)
        {
            PlayerDie();
        }
    }

    void PlayerDie()
    {
        Object.Destroy(gameObject);
        Cursor.lockState = CursorLockMode.None;
    }
}
