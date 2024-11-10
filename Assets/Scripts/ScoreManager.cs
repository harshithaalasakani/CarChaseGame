using TMPro; // For TextMeshPro
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public float score;
    public TextMeshProUGUI scoreText; // Reference to the TextMeshPro element
    private bool isGameActive = true; // Track if the game is active

    void Update()
    {
        if (isGameActive)
        {
            score += Time.deltaTime * 10; // Increment score over time
            scoreText.text = "Score: " + Mathf.Round(score); // Update the score text
        }
    }

    public void StopScore()
    {
        isGameActive = false; // Stop score updates when called
    }

    public void RestartScore()
    {
        score = 0; // Reset the score to 0 after restart
        isGameActive = true; // Reactivate score updates
    }
}

