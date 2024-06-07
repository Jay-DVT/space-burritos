using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    public GameObject customerPrefab; // Assign this from the editor
    public Vector2 spawnPosition; // World coordinates for spawning the customer
    public Vector2 destinationPosition; // World coordinates for the destination
    private float deltaY = .8f;
    public Vector2 exitPosition; // World coordinates for the exit

    public float flatSpawningDelta = 7f;
    public float varianceSpawningDelta = 2f;

    void Start()
    {
        SpawnCustomerAtPosition();
        StartCoroutine(SpawnCustomerRoutine());

    }

    IEnumerator SpawnCustomerRoutine()
    {
        while (true)
        {
            float waitTime = flatSpawningDelta + Random.Range(-varianceSpawningDelta, varianceSpawningDelta);
            yield return new WaitForSeconds(waitTime);

            SpawnCustomerAtPosition();
        }
    }

    void SpawnCustomerAtPosition()
    {
        GameObject customer = SpawnCustomerAtPosition(spawnPosition);
        CustomerMovement customerMovement = customer.GetComponent<CustomerMovement>();
        if (customerMovement != null)
        {
            destinationPosition = new Vector2(destinationPosition.x, destinationPosition.y + Random.Range(-deltaY, deltaY));
            customerMovement.SetDestination(destinationPosition, exitPosition);
        }
        else
        {
            Debug.LogError("Customer prefab does not have a CustomerMovement component");
        }
    }

    GameObject SpawnCustomerAtPosition(Vector2 position)
    {
        return Instantiate(customerPrefab, position, Quaternion.identity);
    }

}
