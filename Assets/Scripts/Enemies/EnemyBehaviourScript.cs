using System.Collections;
using UnityEngine;

public class EnemyBehaviorScript : MonoBehaviour
{
    public EnemySettings enemySettings;

    public float walkSpeed;
    public float attackRange;
    public int difficulty;
    public GameObject player;
    private Rigidbody2D Rigidbody;
    public bool walkCooldown = false;
    public float cooldownTime = 1f;
    public Event EnemyFoundAwayForToLong;

    void Awake()
    {
        walkSpeed = enemySettings.WalkSpeed;
        attackRange = enemySettings.AttackRange;
        difficulty = enemySettings.Difficulty;

        player = GameObject.FindWithTag("Player");

        Rigidbody = transform.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (walkCooldown) { return; }
        if (!player) { return; }
        Vector2 direction = ((Vector2)player.transform.position - Rigidbody.position).normalized;
        Vector2 newPosition = Rigidbody.position + direction * walkSpeed * Time.deltaTime;
        Rigidbody.MovePosition(newPosition);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(StartWalkCooldown());
        }
    }

    private IEnumerator StartWalkCooldown()
    {
        walkCooldown = true;
        yield return new WaitForSeconds(cooldownTime);
        walkCooldown = false;
    }
}
