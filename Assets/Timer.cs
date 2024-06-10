using System;
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

    private string GetFormattedTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60F);
        int seconds = Mathf.FloorToInt(time % 60F);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
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

            timerText.text = "Remaining Time: " + GetFormattedTime(remainingTime);
        }
    }

    public void ResetTimer()
    {
        remainingTime = startTime;
        timerRunning = false;
        if (timerText != null)
        {
            timerText.text = "Remaining Time: " + GetFormattedTime(remainingTime);
        }
    }
    public void StartTimer()
    {
        remainingTime = startTime; // Initialize the remaining time
        timerRunning = true; // Start the timer
    }

    public float GetRemainingTime()
    {
        return remainingTime;
    }

    public void SetRemainingTime(float time)
    {
        remainingTime = time;
        timerRunning = true;
    }

    void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true); // Show the game over panel
        timerRunning = false; // Stop the timer
        Time.timeScale = 0;
    }
}
