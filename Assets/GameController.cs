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

    void Start()
    {
        Time.timeScale = 0;
        if (economy == null)
        {
            economy = FindObjectOfType<Economy>();
        }
    }

    void Update()
    {
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
