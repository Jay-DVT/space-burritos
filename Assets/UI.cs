using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI : MonoBehaviour
{
    public TextMeshProUGUI moneyText;
    private Economy economy; // Reference to your Economy script

    void Start()
    {
        economy = FindObjectOfType<Economy>(); // Find the Economy script in the scene
        if (economy == null)
            Debug.LogError("Economy script not found!");
    }

    void Update()
    {
        if (economy != null)
            if (economy.money >= 10)
            {
                moneyText.text = economy.money.ToString() + " \nPress Space to win!";
            }
            else
            {
                moneyText.text = economy.money.ToString(); // Update the text
            }
    }
}