using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI : MonoBehaviour
{
    private GameController gameController;
    public TextMeshProUGUI moneyText;


    void Start()
    {
        gameController = FindObjectOfType<GameController>();
    }

    void Update()
    {
        int money = Economy.GetInstance().money;
        if (money < gameController.requiredMoney)
        {
            moneyText.text = "Money: $" + money.ToString(); // Update the text
        }
        else
        {
            moneyText.text = "Money: $" + money.ToString() + " \nPress Space to win!";
        }
    }
}