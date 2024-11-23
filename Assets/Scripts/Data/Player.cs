using System;
using Unity.Mathematics;
using UnityEngine;
using System.Collections;
using UnityEngine.UIElements;
using Unity.VisualScripting;

public class Player : MonoBehaviour
{
    public int maxHealth;
    public int health;
    public float maxSpeed;
    public float damageNegation;
    public int damageOnHit;
    public bool hitCooldown;
    public float cooldownTime = 1f;

    public void Start ()
    {
        maxHealth = 100;
        health = maxHealth;
        maxSpeed = 3;
        damageNegation = 0.1f;
        damageOnHit = 3;
        hitCooldown = false;
    }

    public void TakeDamage(int damage)
    {
        if (!hitCooldown)
        {
            StartCoroutine(StartDamageCooldown());

            health = math.max(0, health - damage);

            if (health == 0)
            {
                PlayerDied();
            }
        }
    }

    public void Heal(int amount)
    {
        health = math.min(maxHealth, health + amount);
    }

    public void UpgradeMaxHealth(int amount) 
    {
        maxHealth += amount;
    }

    public void AddSpeed(float amount)
    {
        maxSpeed += amount;
    }

    public void AddDamageNegation(float amount)
    {
        damageNegation += amount;
    }

    public void PlayerDied()
    {
        //TODO Player died function
    }

    private IEnumerator StartDamageCooldown()
    {
        hitCooldown = true;
        yield return new WaitForSeconds(cooldownTime);
        hitCooldown = false;
    }
}
