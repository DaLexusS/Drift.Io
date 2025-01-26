using System.Collections;
using UnityEngine;

public class EnemyBehaviorScript : MonoBehaviour
{
    public EnemySettings enemySettings;

    public float walkSpeed;
    public float attackRange;
    public int damage;

    public GameObject player;
    private Rigidbody2D Rigidbody;
    private CarController2D controller;

    private int PlayerDamageCooldown = 1;
    private float lastHit = 0;
    public float cooldownTime = 4f;

    public bool walkCooldown = false;

    public Event EnemyFoundAwayForToLong;

    void Awake()
    {
        lastHit = Time.time;
        walkSpeed = enemySettings.WalkSpeed;
        attackRange = enemySettings.AttackRange;
        damage = enemySettings.Damage;

        player = GameObject.FindWithTag("Player");
        controller = player.GetComponent<CarController2D>();

        Rigidbody = transform.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (walkCooldown || !player ) { return; }

        float distanceToPlayer = Vector2.Distance(player.transform.position, Rigidbody.position);

        if (distanceToPlayer > attackRange - 0.5f)
        {
            Vector2 direction = ((Vector2)player.transform.position - Rigidbody.position).normalized;
            Vector2 newPosition = Rigidbody.position + direction * walkSpeed * Time.deltaTime;
            Rigidbody.MovePosition(newPosition);
        }
        else
        {
            Rigidbody.velocity = Vector2.zero;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) { return; }

        Vector3 distanceToPlayer = transform.position - collision.transform.position;

        if (distanceToPlayer.magnitude <= attackRange)
        {
            if (controller.canDriveOver) { return; }
            if (lastHit > Time.time) { return; }

            StartCoroutine(StartWalkCooldown());

            lastHit = Time.time + PlayerDamageCooldown;

            Player playerHealth = collision.GetComponent<Player>();
            playerHealth.TakeDamage(damage);
        }
    }

    private IEnumerator StartWalkCooldown()
    {
        walkCooldown = true;

        yield return new WaitForSeconds(cooldownTime);

        walkCooldown = false;
    }
}
