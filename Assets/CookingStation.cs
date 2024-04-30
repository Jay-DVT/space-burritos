using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingStation : MonoBehaviour
{
    public float cookingTime = 5f;  // Time required to cook at this station
    public string productName; // Name of the product that can be cooked here

    public IEnumerator PerformTask(EmployeeBehaviour employee, Transform customerTransform)
    {
        employee.SetTarget(this.transform); // Employee moves to this cooking station
        yield return new WaitForSeconds(cookingTime); // Wait while cooking

        employee.SetTarget(customerTransform); // Employee returns to the customer
        yield return new WaitForSeconds(2f); // Simulate the time taken to hand over the product

        employee.isAvailable = true; // Employee is now available
    }
}
