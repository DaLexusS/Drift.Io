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
    CarController2D controller;
    public bool walkCooldown = false;
    public float cooldownTime = 2f;
    public Event EnemyFoundAwayForToLong;
    private int PlayerDamageCooldown = 1;
    private float lastHit = 0;
    private int damage;
    [SerializeField] public GameObject visual;
    private float centerThreshold = 0.05f;

    void Awake()
    {
        lastHit = Time.time;
        walkSpeed = enemySettings.WalkSpeed;
        attackRange = enemySettings.AttackRange;
        difficulty = enemySettings.Difficulty;
        damage = enemySettings.Damage;

        player = GameObject.FindWithTag("Player");
        controller = player.GetComponent<CarController2D>();

        Rigidbody = transform.GetComponent<Rigidbody2D>();

        CheckPositionRelativeToCamera();
    }

    void FixedUpdate()
    {
        if (walkCooldown || !player) { return; }

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

        Vector2 playerDirection = ((Vector2)player.transform.position - Rigidbody.position).normalized;

        Vector2 oppositeDirection = -playerDirection;

        Vector2 leftDirection = new Vector2(-playerDirection.y, playerDirection.x);
        Vector2 rightDirection = new Vector2(playerDirection.y, -playerDirection.x);

        Vector2 chosenDirection = oppositeDirection;
        int randomChoice = Random.Range(0, 3);
        if (randomChoice == 1) chosenDirection = leftDirection;
        else if (randomChoice == 2) chosenDirection = rightDirection;

        float elapsedTime = 0f;
        while (elapsedTime < cooldownTime)
        {
            Vector2 newPosition = Rigidbody.position + chosenDirection * walkSpeed * Time.deltaTime;
            Rigidbody.MovePosition(newPosition);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        walkCooldown = false;
    }

    private void CheckPositionRelativeToCamera()
    {
        Camera mainCamera = Camera.main;

        if (!mainCamera || visual == null) return;

        Vector3 viewportPosition = mainCamera.WorldToViewportPoint(transform.position);

        if (Mathf.Abs(viewportPosition.x - 0.5f) < centerThreshold) return;

        if (viewportPosition.x < 0.5f && visual.transform.localScale.x < 0)
        {
            MirrorVisual(true);
        }
        else if (viewportPosition.x > 0.5f && visual.transform.localScale.x > 0)
        {
            MirrorVisual(false);
        }
    }

    private void MirrorVisual(bool isLeftSide)
    {
        Vector3 scale = visual.transform.localScale;
        scale.x = isLeftSide ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x);
        visual.transform.localScale = scale;
    }
}
