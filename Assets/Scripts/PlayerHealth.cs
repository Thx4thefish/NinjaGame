using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private float invincibilityDuration = 1f;
    [SerializeField] private TextMeshProUGUI healthText;

    private int currentHealth;
    private float invincibilityTimer;
    private SpriteRenderer spriteRenderer;
    private bool isInvincible;

    public UnityEvent OnDeath;
    public UnityEvent<int> OnHealthChanged;

    private void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateHealthDisplay();
    }

    private void Update()
    {
        // Handle invincibility timer
        if (isInvincible)
        {
            invincibilityTimer -= Time.deltaTime;
            if (invincibilityTimer <= 0)
            {
                isInvincible = false;
                if (spriteRenderer != null)
                {
                    spriteRenderer.color = Color.white;
                }
            }
        }
    }

    public void TakeDamage(int damage)
    {
        if (isInvincible) return;

        currentHealth = Mathf.Max(0, currentHealth - damage);
        OnHealthChanged?.Invoke(currentHealth);
        UpdateHealthDisplay();

        // Start invincibility
        isInvincible = true;
        invincibilityTimer = invincibilityDuration;
        if (spriteRenderer != null)
        {
            spriteRenderer.color = new Color(1, 1, 1, 0.5f); // Semi-transparent
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        currentHealth = Mathf.Min(maxHealth, currentHealth + amount);
        OnHealthChanged?.Invoke(currentHealth);
        UpdateHealthDisplay();
    }

    private void Die()
    {
        OnDeath?.Invoke();
        // You can add death animation, game over screen, etc. here
        Debug.Log("Player died!");
    }

    private void UpdateHealthDisplay()
    {
        if (healthText != null)
        {
            healthText.text = $"Health: {currentHealth}/{maxHealth}";
        }
    }
} 