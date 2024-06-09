using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    public Button upgradeButton; // Reference to the button
    public CookingStation cookingStation; // Reference to the CookingStation script

    public int upgradeCost = 50; // Cost to upgrade the product price
    public int priceIncreaseAmount = 10; // Amount to increase the product price

    private Economy economy;

    void Start()
    {
        if (economy == null)
        {
            economy = FindObjectOfType<Economy>(); // Find the Economy script in the scene
        }

        if (cookingStation == null)
        {
            cookingStation = FindObjectOfType<CookingStation>(); // Find the CookingStation script in the scene
        }

        if (upgradeButton != null)
        {
            upgradeButton.onClick.AddListener(UpgradeProductPrice); // Add listener to the button
        }
    }

    void UpgradeProductPrice()
    {
        if (economy.money >= upgradeCost)
        {
            economy.SubtractMoney(upgradeCost);
            cookingStation.productPrice += priceIncreaseAmount;
            Debug.Log("Product price upgraded to: " + cookingStation.productPrice);
        }
        else
        {
            Debug.Log("Not enough money to upgrade.");
        }
    }
}
