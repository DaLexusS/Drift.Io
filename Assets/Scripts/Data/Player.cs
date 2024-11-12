using System;
using System.Net.NetworkInformation;
using Unity.Mathematics;
using UnityEngine;
public class Player : MonoBehaviour
{
    public int maxHealth;
    public int health;
    public float maxSpeed;
    public float damageNegation;
    public int damageOnHit;

    public int Health
    {
        get { return health; }
        set { health = Math.Clamp(value, 0, maxHealth); }
    }

    public int MaxHealth
    {
        get { return maxHealth; }
        set { maxHealth = math.max(value, maxHealth); }
    }

    public float MaxSpeed
    {
        get { return maxSpeed; }
        set { maxSpeed = math.max(value, maxSpeed); }
    }

    public float DamageNegation
    {
        get { return damageNegation; }
        set { damageNegation = math.max(value, damageNegation); }
    }

    public float DamageOnHit
    {
        get { return damageOnHit; }
        set {}
    }

    public Player ()
    {
        maxHealth = 100;
        health = maxHealth;
        maxSpeed = 3;
        damageNegation = 0.1f;
        damageOnHit = 3;
    }

    public Player(int initialMaxHealth, float initialMaxSpeed, float initialDamageNegation, int initialDamageOnHit)
    {
        maxHealth = initialMaxHealth;
        health = maxHealth;
        maxSpeed = initialMaxSpeed;
        damageNegation = initialDamageNegation;
        damageOnHit = initialDamageOnHit;
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
    }

    public void Heal(int amount)
    {
        health += amount;
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
}
