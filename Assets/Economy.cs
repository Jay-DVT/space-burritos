using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Economy : MonoBehaviour
{
    public int money = 0;

    public void AddMoney(int amount)
    {
        money += amount;
        Debug.Log("Money: " + money);
    }

    public void SubtractMoney(int amount)
    {
        money -= amount;
    }
}
