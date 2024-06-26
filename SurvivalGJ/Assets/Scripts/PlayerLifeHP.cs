using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLifeHP : MonoBehaviour
{    
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;
    public float healthDecreaseInterval = 1f; // Interval in seconds
    private float nextHealthDecreaseTime = 0f;
    private bool isInRain = false;
    public float rainDamageMultiplier = 2f; // Health drops twice as fast 

    private Animator anim;
    private Rigidbody2D rb;

    public int brKampova;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        nextHealthDecreaseTime = Time.time + healthDecreaseInterval;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextHealthDecreaseTime)
        {
            float damage = 1;
            if (isInRain)
            {
                damage *= rainDamageMultiplier;
            }
            TakeDamage((int)damage);
            nextHealthDecreaseTime = Time.time + healthDecreaseInterval;
        }
        if (currentHealth <= 0)
        {
            Die();
            Debug.Log("Umro si.");
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Rain"))
        {
            isInRain = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Rain"))
        {
            isInRain = false;
        }
    }

    public void Die()
    {
        if (brKampova == 0)
        {
            rb.bodyType = RigidbodyType2D.Static;
            anim.SetTrigger("death");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
            Debug.Log("Umro si.");
        }
        else
        {
            currentHealth = maxHealth / 2;
            GameObject kamp = GameObject.FindGameObjectsWithTag("Kamp")[0];
            transform.position = kamp.transform.position;            
            Destroy(kamp);
            brKampova--;
        }        
    }
}
