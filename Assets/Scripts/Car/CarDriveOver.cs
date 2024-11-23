using UnityEditor;
using UnityEngine;

public class CarDriveOver : MonoBehaviour
{

    Player player;
    CarController2D controller;
    private int playerDamageOnEnemyHit;
    private int PlayerDamageCooldown = 1;
    private float lastHit;
    private void Start()
    {
        lastHit = Time.time;
        player = GetComponent<Player>();
        controller = GetComponent<CarController2D>();
        playerDamageOnEnemyHit = player.damageOnHit;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            int enemyDamage = collision.GetComponent<EnemyBehaviorScript>().enemySettings.Damage;
            if (!controller.canDriveOver)
            {
                player.TakeDamage(enemyDamage);

                if (lastHit >= Time.time)
                {
                    lastHit = Time.time + PlayerDamageCooldown;
                }
            }
            else
            {
                Health hitEnemyHealth = collision.GetComponent<Health>();
                hitEnemyHealth.Damage(playerDamageOnEnemyHit);
            }
        }
    }
}
