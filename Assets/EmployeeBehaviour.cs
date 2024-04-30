using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class EmployeeBehaviour : MonoBehaviour
{
    public bool isAvailable = true;
    public Transform currentTarget;
    public float speed = 2f;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

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
    }

    void MoveTowardsTarget()
    {
        if (Vector2.Distance(transform.position, currentTarget.position) > 0.1f)
        {
            Vector2 direction = (currentTarget.position - transform.position).normalized;
            rb.velocity = direction * speed;  // Use Rigidbody2D to set velocity

        }
        else
        {
            rb.velocity = Vector2.zero;  // Stop moving when the target is reached or null
            currentTarget = null;
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Employee"))
        {
            // Optional: Additional logic when colliding with another employee
            Debug.Log("Collision with another employee!");
        }
    }

}
