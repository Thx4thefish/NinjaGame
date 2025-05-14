using UnityEngine;

public class Launcher : MonoBehaviour
{
    [SerializeField] private float launchForce = 15f; // Adjustable launch force in the inspector
    [SerializeField] private bool resetVelocity = true; // Whether to reset velocity before launching
    [SerializeField] private int pointsToAdd = 100; // Points to add when launched

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision is from above
        foreach (ContactPoint2D contact in collision.contacts)
        {
            // If the contact normal is pointing down (y < 0), the collision is from above
            if (contact.normal.y < 0)
            {
                Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    // Reset velocity if enabled
                    if (resetVelocity)
                    {
                        rb.linearVelocity = Vector2.zero;
                    }
                    
                    // Apply upward force
                    rb.AddForce(Vector2.up * launchForce, ForceMode2D.Impulse);
                    
                    // Add points if it's the player
                    
                    
                    // Optional: Add a debug log
                    Debug.Log($"Launched {collision.gameObject.name} with force {launchForce}");
                }
                break; // Exit after first valid contact
            }
        }
    }
} 