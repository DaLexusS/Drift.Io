
using Unity.Mathematics;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public int maxHealth;
    public int health;
    public float maxSpeed;
    public float damageNegation;
    public int damage;
    public bool hitCooldown;
    public float cooldownTime = 1f;

    public float roundPlayerLevel;
    public float roundXp;
    public float xpNeededToLevel;

    public int enemiesKilled = 0;

    public static event UnityAction<int> OnHealthChanged;
    public static event UnityAction<float> OnXpChanged;
    public UnityEvent<int> killedEnemiesEvent;
    CarController2D controller;

    private void Awake()
    {
        controller = GetComponent<CarController2D>();
    }

    public void Start ()
    {
        maxHealth = 100;
        health = maxHealth;
        maxSpeed = 3;
        damageNegation = 0.1f;
        damage = 3;
        hitCooldown = false;

        roundPlayerLevel = 0;
        roundXp = 0;
        xpNeededToLevel = LevelHandler.ReturnStaticLevel();
    }

    public void TakeDamage(int damage)
    {
        if (!hitCooldown)
        {
            StartCoroutine(StartDamageCooldown());

            health = math.max(0, health - damage);

            OnHealthChanged.Invoke(health);

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
        OnXpChanged.Invoke(roundXp);
        if (roundXp >= xpNeededToLevel)
        {
            LevelUp();
        }
    }

    public void LevelUp()
    {
        roundXp = 0;
        roundPlayerLevel += 1;
        xpNeededToLevel = LevelHandler.ReturnStaticLevel();
        OnXpChanged.Invoke(roundXp);
    }

    public void UpdateKilledEnemies(int amount)
    {
        enemiesKilled += amount;
    }

    private IEnumerator StartDamageCooldown()
    {
        hitCooldown = true;
        yield return new WaitForSeconds(cooldownTime);
        hitCooldown = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Enemy") { return; }

        if (!controller.canDriveOver) { return; }

        else
        {
            EnemyHealth enemyHealth = collision.GetComponent<EnemyHealth>();

            if (enemyHealth == null) { return; }

            enemyHealth.Damage(damage);

            killedEnemiesEvent.Invoke(enemiesKilled);
        }
    }
}
