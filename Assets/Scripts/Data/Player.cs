using System;
using Unity.Mathematics;
using UnityEngine;
using System.Collections;
using UnityEngine.UIElements;
using Unity.VisualScripting;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public int maxHealth;
    public int health;
    public float maxSpeed;
    public float damageNegation;
    public int damageOnHit;
    public bool hitCooldown;
    public float cooldownTime = 1f;

    public float roundPlayerLevel;
    public float roundXp;
    public float xpNeededToLevel;

    public void Start ()
    {
        maxHealth = 100;
        health = maxHealth;
        maxSpeed = 3;
        damageNegation = 0.1f;
        damageOnHit = 3;
        hitCooldown = false;

        roundPlayerLevel = 0;
        roundXp = 0;
        // xpNeededToLevel = LevelHandler.ReturnXPCalculation(roundPlayerLevel);
        xpNeededToLevel = LevelHandler.ReturnStaticLevel();
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
        SceneManager.LoadScene("Menu");
    }

    public void AddXp(float amount) 
    {
        roundXp += amount;
        if (roundXp >= xpNeededToLevel)
        {
            LevelUp();
        }
    }

    public void LevelUp()
    {
        roundXp = 0;
        roundPlayerLevel += 1;
        //xpNeededToLevel = LevelHandler.ReturnXPCalculation(roundPlayerLevel);
        xpNeededToLevel = LevelHandler.ReturnStaticLevel();
    }

    private IEnumerator StartDamageCooldown()
    {
        hitCooldown = true;
        yield return new WaitForSeconds(cooldownTime);
        hitCooldown = false;
    }
}
