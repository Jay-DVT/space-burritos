using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class EmployeeBehaviour : MonoBehaviour
{
    public bool isAvailable = true;
    public Transform currentTarget;
    public float speed = 5f;

    // Update is called once per frame
    void Update()
    {
        if (currentTarget != null)
        {
            MoveTowardsTarget();
        }

    }

    public void SetTarget(Transform target)
    {
        currentTarget = target;
        isAvailable = false;
    }

    void MoveTowardsTarget()
    {
        if (Vector3.Distance(transform.position, currentTarget.position) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, currentTarget.position, speed * Time.deltaTime);
        }
        else
        {
            currentTarget = null;
            isAvailable = true;
        }

    }
}
