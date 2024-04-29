using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerMovement : MonoBehaviour
{
    public float speed = 5f;
    private Vector3 destination;
    private Vector3 exitPosition; // You can assign this via the inspector or programmatically
    public float waitTime = 5f; // Time to wait in seconds

    public void SetDestination(Vector2 newDestination, Vector2 newExit)
    {
        destination = newDestination;
        exitPosition = newExit;
        StartCoroutine(MoveAndExitRoutine());
    }

    IEnumerator MoveAndExitRoutine()
    {
        // Move towards the initial destination
        Debug.Log("Moving towards destination");
        while (Vector3.Distance(transform.position, destination) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
            yield return null;
        }
        // Wait for employee to finish work

        // Move towards the exit
        while (Vector3.Distance(transform.position, exitPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, exitPosition, speed * Time.deltaTime);
            yield return null;
        }
        Destroy(gameObject);

        Debug.Log("Reached exit");
    }


}
