using Unity.Mathematics;
using UnityEngine;


public class Health : MonoBehaviour
{
    public EnemySettings enemySettings;
    public int health;
    public float damageResistance;

    private void Start()
    {
        health = enemySettings.Health;
        damageResistance = enemySettings.DamageResistance;
    }

    public void Damage(int amount)
    {
        int calculateDamage = math.max(0, health - amount);
        health = calculateDamage;

        CheckAlive();
    }

    private void CheckAlive()
    {
        if (health <= 0) { Object.Destroy(gameObject); }
    }
}
