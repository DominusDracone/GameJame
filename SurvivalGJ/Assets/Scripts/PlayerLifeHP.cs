using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLifeHP : MonoBehaviour
{    
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;
    public float healthDecreaseInterval = 1f; // Interval in seconds
    private float nextHealthDecreaseTime = 0f;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        nextHealthDecreaseTime = Time.time + healthDecreaseInterval;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextHealthDecreaseTime)
        {
            TakeDamage(1); // Decrease health by 1 every second
            nextHealthDecreaseTime = Time.time + healthDecreaseInterval;
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0)
        {
            currentHealth = 0; // Ensure health doesn't go below 0
        }
        healthBar.SetHealth(currentHealth);
    }
}
