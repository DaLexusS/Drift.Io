using Unity.Mathematics;
using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{
    public EnemySettings enemySettings;
    public int health;
    public float damageResistance;
    public bool damagedCooldown = false;
    public float cooldownTime = 0.5f;
    GameObject player;
    

    private void Start()
    {
        health = enemySettings.Health;
        damageResistance = enemySettings.DamageResistance;
        player = GameObject.FindWithTag("Player");
    }

    public void Damage(int amount)
    {
        if (damagedCooldown) 
        {
            return;
        }

        StartCoroutine(StartDamageCooldown());

        int calculateDamage = math.max(0, health - amount);
        health = calculateDamage;

        CheckAlive();
    }

    public void Remove()
    {
        Object.Destroy(gameObject);
    }

    private void CheckAlive()
    {
        if (health <= 0) 
        {
            player.GetComponent<Player>().AddXp(enemySettings.xpForKill);
            Object.Destroy(gameObject); 
        }
    }

    public IEnumerator StartDamageCooldown()
    {
        damagedCooldown = true;
        yield return new WaitForSeconds(cooldownTime);
        damagedCooldown = false;
    }
}
