using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float detectionRange = 10f;
    [SerializeField] private float attackRange = 1f;

    [Header("Attack Settings")]
    [SerializeField] private int damageAmount = 1;
    [SerializeField] private float attackCooldown = 1f;
    private float lastAttackTime;

    private Transform player;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (player == null)
        {
            Debug.LogError("Enemy: Player not found! Make sure the player has the 'Player' tag.");
        }
    }

    private void Update()
    {
        if (player == null) return;

        // Calculate distance to player
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Only move if player is within detection range
        if (distanceToPlayer <= detectionRange)
        {
            // Calculate direction to player
            Vector2 direction = (player.position - transform.position).normalized;

            // Move towards player
            rb.linearVelocity = direction * moveSpeed;

            // Flip sprite based on movement direction
            if (spriteRenderer != null)
            {
                spriteRenderer.flipX = direction.x < 0;
            }
        }
        else
        {
            // Stop moving if player is too far
            rb.linearVelocity = Vector2.zero;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Check if enough time has passed since last attack
            if (Time.time >= lastAttackTime + attackCooldown)
            {
                // Deal damage to player
                PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(damageAmount);
                    lastAttackTime = Time.time;
                }
            }
        }
    }
} 