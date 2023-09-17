using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MesaFood : MonoBehaviour
{

   // private List<SpriteRenderer> listMesaFoodImages;
    Sprite emptyPlateImage;
    Sprite fullPlateImage;
    void Start()
    {
        emptyPlateImage = Resources.Load<Sprite>("Illustration/MockedItems/emptyPlate");
        fullPlateImage = Resources.Load<Sprite>("Illustration/MockedItems/fullPlate");

        isEmpty = true;
    }

   public bool isEmpty;

    void Update()
    {
        if (isEmpty)
        {
            GetComponent<SpriteRenderer>().sprite = emptyPlateImage;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = fullPlateImage;
        }
    }

    public void AddFoodToMesa()
    {
        isEmpty = false;
    }

}
