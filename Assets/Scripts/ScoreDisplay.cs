using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private string scorePrefix = "Score: ";

    private void Start()
    {
        if (scoreText == null)
        {
            scoreText = GetComponent<TextMeshProUGUI>();
            if (scoreText == null)
            {
                Debug.LogError("ScoreDisplay: No TextMeshProUGUI component found!");
                return;
            }
        }

        // Subscribe to score changes
        Gm.Instance.OnScoreChanged.AddListener(UpdateScore);
        
        // Set initial score
        UpdateScore(Gm.Instance.Score);
    }

    private void OnDestroy()
    {
        // Unsubscribe from score changes
        if (Gm.Instance != null)
        {
            Gm.Instance.OnScoreChanged.RemoveListener(UpdateScore);
        }
    }

    private void UpdateScore(int newScore)
    {
        scoreText.text = scorePrefix + newScore.ToString();
    }
} 