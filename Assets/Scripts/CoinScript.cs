using UnityEngine;

public class CoinScript : MonoBehaviour
{
    [SerializeField] private int pointsToAdd = 1; // Points to add when collected
    [SerializeField] private bool destroyOnCollect = true; // Whether to destroy the coin after collection

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object that entered is the player
        if (other.CompareTag("Player"))
        {
            // Add points
            Gm.Instance.AddScore(pointsToAdd);
            
            // Optional: Play collection sound
            // AudioManager.Instance?.Play("CoinCollect");
            
            // Optional: Play collection animation
            // GetComponent<Animator>()?.Play("Collect");
            
            // Destroy the coin if enabled
            if (destroyOnCollect)
            {
                Destroy(gameObject);
            }
        }
    }
}
