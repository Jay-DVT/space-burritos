using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public int requiredMoney = 10;
    public GameObject endGamePanel;
    public GameObject startGamePanel;

    private Economy economy;
    public GameObject pauseMenuPanel;
    public GameObject upgradeMenuPanel;
    private bool isPaused = false;

    void Start()
    {
        Time.timeScale = 0;
        if (economy == null)
        {
            economy = FindObjectOfType<Economy>();
        }
    }

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
        if (economy.money >= requiredMoney)
        {
            endGamePanel.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void StartGame()
    {
        startGamePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }
}
