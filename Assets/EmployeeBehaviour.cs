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
    private SpriteRenderer spriteRenderer;
    private GameObject carriedFood;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTarget != null)
        {
            MoveTowardsTarget();
        }
        if (rb.velocity.x != 0)
        {
            spriteRenderer.flipX = rb.velocity.x > 0;
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
            if (carriedFood != null && currentTarget != null && currentTarget.CompareTag("Customer"))
            {
                // Si el empleado ha llegado al cliente, entregar la comida
                DeliverFood();
            }
            currentTarget = null;
        }
    }

    public void PickUpFood(GameObject food)
    {
        carriedFood = food;
        carriedFood.transform.SetParent(this.transform); // Hacer que la comida sea hijo del empleado

        // Ajustar la posición relativa de la comida a la derecha del empleado
        float offset = 0.5f; // Ajusta este valor según sea necesario para posicionar la comida
        if (spriteRenderer.flipX)
        {
            carriedFood.transform.localPosition = new Vector3(-offset, 0, 0); // Si el sprite está volteado, poner la comida a la izquierda
        }
        else
        {
            carriedFood.transform.localPosition = new Vector3(offset, 0, 0); // De lo contrario, poner la comida a la derecha
        }
    }

    public void DeliverFood()
    {
        if (carriedFood != null)
        {
            // Lógica para entregar la comida al cliente
            Destroy(carriedFood);
            carriedFood = null;
            // Aquí puedes agregar más lógica si es necesario, como animaciones o efectos
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Employee"))
        {
            Debug.Log("Collision with another employee!");
        }
    }
}
