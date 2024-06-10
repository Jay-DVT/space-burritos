using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public int requiredMoney;
    public CountdownTimer countdownTimer;
    public GameObject endGamePanel;
    public GameObject startGamePanel;

    public GameObject pauseMenuPanel;
    public GameObject upgradeMenuPanel;
    public GameObject introPanel;
    private bool isPaused = false;


    public void Resume()
    {
        pauseMenuPanel.SetActive(false);
        upgradeMenuPanel.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void UpgradeMenu()
    {
        upgradeMenuPanel.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    private void Pause()
    {
        pauseMenuPanel.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    private void HandlePauseInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused) Resume();
            else Pause();
        }
    }
    void Update()
    {
        HandlePauseInput();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CheckMoneyAndEndGame();
        }
    }

    void CheckMoneyAndEndGame()
    {
        if (Economy.GetInstance().money >= requiredMoney)
        {
            endGamePanel.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void StartIntro()
    {
        startGamePanel.SetActive(false);
        introPanel.SetActive(true);
    }

    public void StartGame()
    {
        startGamePanel.SetActive(false);
        introPanel.SetActive(false);
        Time.timeScale = 1;
        if (countdownTimer != null)
        {
            Debug.Log("Starting countdown timer");
            countdownTimer.StartTimer(); // Start the countdown timer
        }

    }

    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }
}
