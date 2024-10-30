using System;
using Unity.Mathematics;
using UnityEngine;
public class Player : MonoBehaviour
{
    private int maxHealth;
    private int health;
    private float maxSpeed;
    private float damageNegation;

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

    public Player()
    {
        maxHealth = 100;
        health = maxHealth;
        maxSpeed = 3;
        damageNegation = 0.1f;
    }

    public Player(int initialMaxHealth, float initialMaxSpeed, float initialDamageNegation)
    {
        maxHealth = initialMaxHealth;
        health = maxHealth;
        maxSpeed = initialMaxSpeed;
        damageNegation = initialDamageNegation;
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
