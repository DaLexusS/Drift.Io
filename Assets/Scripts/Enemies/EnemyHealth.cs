using Unity.Mathematics;
using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour
{
    public EnemySettings enemySettings;
    public int health;
    public float damageResistance;
    [SerializeField] public bool damagedCooldown = false;
    public ParticleSystem bloodSplat;
    public float cooldownTime = 0.5f;
    public GameObject enemyVisual;
    public GameObject player;
    public GameObject pyhsicalHitbox;
    private bool canDamage = true;
    private float timeBeforeDestroy = 1.3f;

    private void Start()
    {
        health = enemySettings.Health;
        damageResistance = enemySettings.DamageResistance;
        player = GameObject.FindWithTag("Player");
    }

    public void Damage(int amount)
    {
        if (damagedCooldown || !canDamage) 
        {
            return;
        }

        StartCoroutine(StartDamageCooldown());

        int calculateDamage = math.max(0, health - amount);
        health = calculateDamage;

        CheckAlive();
    }

    public void DamageWhenFar()
    {
        Destroy(gameObject);
    }

    private void CheckAlive()
    {
        if (health <= 0) 
        {
            canDamage = true;
            player.GetComponent<Player>().AddXp(enemySettings.xpForKill);
            enemyVisual.GetComponent<Animator>().SetTrigger("OnDeath");
            player.GetComponent<Player>().enemiesKilled++;
            StartCoroutine(AnimateDeath());
        }
    }

    private void ChangeVisualLayoutOrder()
    {
        foreach (Transform child in enemyVisual.transform)
        {
            child.gameObject.GetComponent<SpriteRenderer>().sortingOrder = -2;
        }
    }
    public IEnumerator StartDamageCooldown()
    {
        damagedCooldown = true;
        yield return new WaitForSeconds(cooldownTime);
        damagedCooldown = false;
    }

    private IEnumerator AnimateDeath()
    {
        bloodSplat.Play();
        damagedCooldown = true;
        transform.gameObject.GetComponent<Rigidbody2D>().simulated = false;
        ChangeVisualLayoutOrder();
        Destroy(pyhsicalHitbox);
        yield return new WaitForSeconds(timeBeforeDestroy);
        Destroy(gameObject);
    }
}
