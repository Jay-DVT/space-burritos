using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Economy : MonoBehaviour
{
    public static Economy Instance { get; private set; }

    public int money = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Ensure it persists across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy this instance if another already exists
        }
    }

    public static Economy GetInstance()
    {
        if (Instance == null)
        {
            GameObject singleton = new GameObject("Economy");
            Instance = singleton.AddComponent<Economy>();
            DontDestroyOnLoad(singleton);
        }
        return Instance;
    }

    public void AddMoney(int amount)
    {
        money += amount;
    }

    public bool MakeTransaction(int amount)
    {
        if (money >= amount)
        {
            money -= amount;
            return true;
        }
        return false;
    }

}
