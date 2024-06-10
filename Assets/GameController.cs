using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
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
    public GameObject saveGamePanel;
    public GameObject loadGamePanel;
    private bool isPaused = false;
    public List<CookingStation> cookingStations;


    public void Resume()
    {
        pauseMenuPanel.SetActive(false);
        upgradeMenuPanel.SetActive(false);
        saveGamePanel.SetActive(false);
        loadGamePanel.SetActive(false);
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
            countdownTimer.StartTimer(); // Start the countdown timer
        }

    }

    public void SaveGame(int slot)
    {
        GameData data = new GameData();
        data.cookingStations = new List<CookingStationData>();

        foreach (var station in cookingStations)
        {
            data.cookingStations.Add(station.SaveData());
        }

        data.remainingTime = countdownTimer.GetRemainingTime();
        data.money = Economy.GetInstance().money; // Save the player's money

        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString($"savegame{slot}", json);
        PlayerPrefs.Save();
        Debug.Log($"Game Saved in slot {slot}");
        Resume();
    }

    public void LoadGame(int slot)
    {
        string json = PlayerPrefs.GetString($"savegame{slot}", null);
        if (!string.IsNullOrEmpty(json))
        {
            GameData data = JsonUtility.FromJson<GameData>(json);

            for (int i = 0; i < data.cookingStations.Count; i++)
            {
                cookingStations[i].LoadData(data.cookingStations[i]);
            }

            countdownTimer.SetRemainingTime(data.remainingTime);
            Economy.GetInstance().money = data.money; // Load the player's money
            Debug.Log($"Game Loaded from slot {slot}");
        }
        else
        {
            Debug.Log($"No save file found in slot {slot}");
        }
        if (!startGamePanel.activeSelf) Resume();
        else StartGame();
    }

    public void ShowSaveGamePanel()
    {
        saveGamePanel.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ShowLoadGamePanel()
    {
        loadGamePanel.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void CloseSaveGamePanel()
    {
        saveGamePanel.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void CloseLoadGamePanel()
    {
        loadGamePanel.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void ResetGame()
    {
        InitializeGame();
    }

    private void InitializeGame()
    {
        // Reset all game elements to their initial state
        Economy.GetInstance().money = 0; // Reset money

        foreach (var station in cookingStations)
        {
            station.ResetStation(); // Ensure each CookingStation has a ResetStation method
        }

        if (countdownTimer != null)
        {
            countdownTimer.ResetTimer(); // Ensure CountdownTimer has a ResetTimer method
        }

        // Hide end game and other panels
        startGamePanel.SetActive(true);
        endGamePanel.SetActive(false);
        saveGamePanel.SetActive(false);
        loadGamePanel.SetActive(false);
        pauseMenuPanel.SetActive(false);
        upgradeMenuPanel.SetActive(false);
        introPanel.SetActive(false);
    }

}
