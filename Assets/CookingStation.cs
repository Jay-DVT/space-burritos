using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingStation : MonoBehaviour
{
    public float cookingTime = 5f;  // Time required to cook at this station
    public string productName; // Name of the product that can be cooked here
    public int productPrice; // Price of the product
    public GameObject foodPrefab;

    private Economy economy;

    void Start()
    {
        if (economy == null)
        {
            economy = FindObjectOfType<Economy>();
        }
    }

    public IEnumerator PerformTask(EmployeeBehaviour employee, Transform customerTransform)
    {
        employee.SetTarget(this.transform);
        yield return new WaitForSeconds(cookingTime);

        GameObject food = SpawnFood();

        employee.PickUpFood(food);

        employee.SetTarget(customerTransform);
        yield return new WaitForSeconds(2f);

        while (Vector2.Distance(employee.transform.position, customerTransform.position) > 0.1f)
        {
            yield return null;
        }

        economy.AddMoney(productPrice);

        employee.isAvailable = true;
    }

    GameObject SpawnFood()
    {
        return Instantiate(foodPrefab, transform.position, Quaternion.identity);
    }
}
