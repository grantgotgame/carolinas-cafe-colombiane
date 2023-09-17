using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveFood : MonoBehaviour
{
    private CircleCollider2D circleCollider; // Reference to the Circle Collider

    private List<SpriteRenderer> listFoodImages;


    Sprite stoveFullImage;
    Sprite fullPlateImage;

    public bool isEmpty;

    void Start()
    {
        stoveFullImage = Resources.Load<Sprite>("Illustration/MockedItems/stoveFull");
      //  fullPlateImage = Resources.Load<Sprite>("Illustration/MockedItems/fullPlate");
        isEmpty = true;

        circleCollider = GetComponent<CircleCollider2D>();

        if (circleCollider == null)
        {
            Debug.LogError("Circle Collider 2D component not found on this GameObject.");
        }
    }
    bool isPrepared;

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
                Debug.Log("Mouse click detected inside the circle collider!");

                // Add your custom logic here, e.g., change color, perform an action, etc.

              bool isAdded =  gameObject.transform.parent.GetComponent<StoveFoodContainer>().AddFoodInMesaContainer(isPrepared);
                if (isAdded)
                {
                    //TODO perform timer
                    isEmpty = true;
                    isPrepared = false;
                }
            }
        }

        if (isEmpty)
        {
            GetComponent<SpriteRenderer>().sprite = null;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = stoveFullImage;
        }

    }

    public void CreateArepas()
    {
        isEmpty = false;
        StartCoroutine(PrepareTimerArepas());
    }

    IEnumerator PrepareTimerArepas()
    {


        yield return new   WaitForSeconds(1f);

        isPrepared = true;
    }
   
}
