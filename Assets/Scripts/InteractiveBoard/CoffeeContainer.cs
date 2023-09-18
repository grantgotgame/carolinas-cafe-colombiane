using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeContainer : MonoBehaviour
{
    // private List<SpriteRenderer> listMesaFoodImages;
    Sprite coffeImage;
 
    void Start()
    {
        coffeImage = Resources.Load<Sprite>("Illustration/MockedItems/coffee");
    }

    // Update is called once per frame
    void Update()
    {
        if (!isCoffeReady)
        {
            GetComponent<SpriteRenderer>().sprite = null;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = coffeImage;
        }
    }


 bool   isCoffeReady;

    //TODO add a timer before to change the image of the coffee
    public void AddCoffee()
    {
        isCoffeReady = true;
    }
}
