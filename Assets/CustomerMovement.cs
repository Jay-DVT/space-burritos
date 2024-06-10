using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerMovement : MonoBehaviour
{
    public float speed = 5f;
    private Vector3 destination;
    private Vector3 exitPosition; // You can assign this via the inspector or programmatically
    public float waitTime = 1f; // Time to wait in seconds

    public void SetDestination(Vector2 newDestination, Vector2 newExit)
    {
        destination = newDestination;
        exitPosition = newExit;
        StartCoroutine(MoveAndExitRoutine());
    }

    IEnumerator MoveAndExitRoutine()
    {
        // Move towards the initial destination
        while (Vector3.Distance(transform.position, destination) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
            yield return null;
        }
        // Wait for employee to finish work
        yield return StartCoroutine(CallEmployee());

        // Move towards the exit
        while (Vector3.Distance(transform.position, exitPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, exitPosition, speed * Time.deltaTime);
            yield return null;
        }
        Destroy(gameObject);

    }


    private IEnumerator CallEmployee()
    {
        EmployeeBehaviour employee = null;
        while (employee == null)
        {
            yield return new WaitForSeconds(waitTime);
            employee = FindAvailableEmployee();
        }

        employee.isAvailable = false;

        if (employee != null)
        {
            employee.SetTarget(this.transform);
            yield return new WaitForSeconds(waitTime);

            CookingStation chosenStation = FindAvailableStation();
            yield return StartCoroutine(chosenStation.PerformTask(employee, this.transform));
        }
    }

    private EmployeeBehaviour FindAvailableEmployee()
    {
        EmployeeBehaviour[] employees = FindObjectsOfType<EmployeeBehaviour>();
        foreach (EmployeeBehaviour employee in employees)
        {
            if (employee.isAvailable)
            {
                return employee;
            }
        }
        return null;
    }

    private CookingStation FindAvailableStation()
    {
        CookingStation[] stations = FindObjectsOfType<CookingStation>();
        int index = Random.Range(0, stations.Length);
        return stations[index];
    }

}
