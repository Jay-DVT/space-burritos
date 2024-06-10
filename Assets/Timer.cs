using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountdownTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText; // Reference to the UI Text element
    public float startTime = 300f; // Total countdown time in seconds
    public GameObject gameOverPanel; // Reference to the Game Over Panel

    private float remainingTime; // Time remaining
    private bool timerRunning = false; // Flag to check if the timer is running

    void Start()
    {
        if (timerText == null)
        {
            Debug.LogError("TimerText is not assigned.");
        }
        if (gameOverPanel == null)
        {
            Debug.LogError("GameOverPanel is not assigned.");
        }
        gameOverPanel.SetActive(false); // Ensure the game over panel is inactive at the start
    }

    void Update()
    {
        if (timerRunning && timerText != null && remainingTime > 0)
        {
            remainingTime -= Time.deltaTime; // Decrement the remaining time by the time passed since the last frame

            if (remainingTime <= 0)
            {
                remainingTime = 0;
                ShowGameOverPanel();
            }

            int minutes = Mathf.FloorToInt(remainingTime / 60F);
            int seconds = Mathf.FloorToInt(remainingTime % 60F);
            timerText.text = "Remaining Time: " + string.Format("{0:00}:{1:00}", minutes, seconds);
            Debug.Log("Time: " + timerText.text);
        }
    }

    public void StartTimer()
    {
        remainingTime = startTime; // Initialize the remaining time
        timerRunning = true; // Start the timer
    }

    void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true); // Show the game over panel
        timerRunning = false; // Stop the timer
        Time.timeScale = 0;
    }
}
