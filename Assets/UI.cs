using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI : MonoBehaviour
{
    public TextMeshProUGUI moneyText;


    void Update()
    {
        int money = Economy.GetInstance().money;
        if (money >= 10)
        {
            moneyText.text = "Money: $" + money.ToString() + " \nPress Space to win!";
        }
        else
        {
            moneyText.text = "Money: $" + money.ToString(); // Update the text
        }
    }
}