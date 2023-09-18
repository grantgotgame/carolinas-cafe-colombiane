using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MesaFood : MonoBehaviour
{

    // private List<SpriteRenderer> listMesaFoodImages;
    Sprite emptyPlateImage;
    Sprite fullPlateImage;
    Sprite fullJamPlateImage;
    void Start()
    {
        emptyPlateImage = Resources.Load<Sprite>("Illustration/MockedItems/emptyPlate");
        fullPlateImage = Resources.Load<Sprite>("Illustration/MockedItems/fullPlate");
        fullJamPlateImage = Resources.Load<Sprite>("Illustration/MockedItems/fullJamPlate");

        isEmpty = true;
    }

    public bool isEmpty;
    public bool isJamAdded;

    void Update()
    {
        if (isEmpty)
        {
            GetComponent<SpriteRenderer>().sprite = emptyPlateImage;
        }
        else
        {
            if (!isJamAdded)
            {
                GetComponent<SpriteRenderer>().sprite = fullPlateImage;
            }
            else
            {
                GetComponent<SpriteRenderer>().sprite = fullJamPlateImage;
            }
        }
    }

    public void AddFoodToMesa()
    {
        isEmpty = false;
    }

    public void AddJamToFood()
    {
        isJamAdded = true;
    }

    //This method is called when you want trash the dish or when you give the dish to a customer
    public void RemoveFoodFromMesa()
    {
        isEmpty = true;
        isJamAdded = false;
    }



}
