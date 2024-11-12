using UnityEngine;

public class CarDriveOver : MonoBehaviour
{

    Player player;
    CarController2D controller;
    private int playerDamageOnEnemyHit;
    private void Start()
    {
        player = GetComponent<Player>();
        controller = GetComponent<CarController2D>();
        playerDamageOnEnemyHit = player.damageOnHit;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!controller.canDriveOver) { return; }

        if (collision.CompareTag("Enemy"))
        {
            Health hitEnemyHealth = collision.GetComponent<Health>();
            hitEnemyHealth.Damage(playerDamageOnEnemyHit);
        }
    }
}
