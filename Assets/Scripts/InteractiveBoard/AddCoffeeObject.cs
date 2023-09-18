using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddCoffeeObject : MonoBehaviour
{
    CircleCollider2D circleCollider; // Reference to the Circle Collider
    [SerializeField] CoffeeContainer coffeeContainer; // Reference to the Circle Collider

    void Start()
    {
        // Get a reference to the Circle Collider component
        circleCollider = GetComponent<CircleCollider2D>();

        if (circleCollider == null)
        {
            Debug.LogError("Circle Collider 2D component not found on this GameObject.");
        }
    }

    private void Update()
    {
        // Check for mouse click
        if (Input.GetMouseButtonDown(0)) // 0 for left mouse button
        {
            // Get the mouse position in world coordinates
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Check if the mouse click is within the circle collider
            if (circleCollider.OverlapPoint(mousePosition))
            {
                // The click is inside the circle collider
                Debug.Log("trying to AddCoffee!");

                // Add your custom logic here, e.g., change color, perform an action, etc.

                coffeeContainer.AddCoffee();

            }
        }
    }
}
