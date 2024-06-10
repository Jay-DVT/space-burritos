using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingStation : MonoBehaviour
{
    public UpgradeData upgradeData; // Reference to the ScriptableObject holding upgrade data
    private float currentCookingTime;
    private int currentProductValue;
    public int speedUpgradeLevel = 0;
    public int valueUpgradeLevel = 0;
    private const int maxUpgradeLevel = 5;

    void Start()
    {
        currentCookingTime = upgradeData.baseCookingTime;
        currentProductValue = upgradeData.baseProductPrice;
    }


    public IEnumerator PerformTask(EmployeeBehaviour employee, Transform customerTransform)
    {
        employee.SetTarget(this.transform);
        yield return new WaitForSeconds(currentCookingTime);

        GameObject food = SpawnFood();

        employee.PickUpFood(food);

        employee.SetTarget(customerTransform);
        yield return new WaitForSeconds(2f);

        while (Vector2.Distance(employee.transform.position, customerTransform.position) > 0.1f)
        {
            yield return null;
        }

        Economy.GetInstance().AddMoney(currentProductValue);

        employee.isAvailable = true;
    }

    GameObject SpawnFood()
    {
        return Instantiate(upgradeData.foodPrefab, transform.position, Quaternion.identity);
    }

    public bool CanUpgradeCookingSpeed()
    {
        return speedUpgradeLevel < maxUpgradeLevel;
    }

    public void UpgradeCookingSpeed()
    {
        int currentSpeedUpgradeCost = Mathf.CeilToInt(upgradeData.baseUpgradeCost * Mathf.Pow(upgradeData.upgradeCostMultiplier, speedUpgradeLevel));
        if (Economy.GetInstance().money >= currentSpeedUpgradeCost && Economy.GetInstance().MakeTransaction(currentSpeedUpgradeCost))
        {
            currentCookingTime -= upgradeData.cookingTimeUpgrade;
            speedUpgradeLevel++;
        }
        else
        {
            Debug.Log("Not enough money for speed upgrade!");
        }
    }

    public bool CanUpgradeProductValue()
    {
        return valueUpgradeLevel < maxUpgradeLevel;
    }

    public void UpgradeProductValue()
    {
        int currentValueUpgradeCost = Mathf.CeilToInt(upgradeData.baseUpgradeCost * Mathf.Pow(upgradeData.upgradeCostMultiplier, valueUpgradeLevel));
        if (Economy.GetInstance().money >= currentValueUpgradeCost && Economy.GetInstance().MakeTransaction(currentValueUpgradeCost))
        {
            currentProductValue += upgradeData.productValueUpgrade;
            valueUpgradeLevel++;
        }
        else
        {
            Debug.Log("Not enough money for value upgrade!");
        }
    }

    public CookingStationData SaveData()
    {
        return new CookingStationData
        {
            productName = upgradeData.productName,
            cookingTime = currentCookingTime,
            productPrice = currentProductValue,
            speedUpgradeLevel = speedUpgradeLevel,
            valueUpgradeLevel = valueUpgradeLevel
        };
    }

    public void LoadData(CookingStationData data)
    {
        currentCookingTime = data.cookingTime;
        currentProductValue = data.productPrice;
        speedUpgradeLevel = data.speedUpgradeLevel;
        valueUpgradeLevel = data.valueUpgradeLevel;
    }

}
