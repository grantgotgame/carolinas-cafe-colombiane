using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MesaFoodContainer : MonoBehaviour
{
    public List<GameObject> mesaFoodList = new List<GameObject>();

    private void Start()
    {
        foreach (Transform mesaFood in gameObject.transform)
        {
            mesaFood.gameObject.AddComponent<MesaFood>();
            mesaFoodList.Add(mesaFood.gameObject);
        }
    }


    bool freeSlot;

    public bool CreateArepas()
    {
        freeSlot = CheckFreeSlot();
        if (!freeSlot)
        {
            Debug.Log("mesa food container is full");
        }
        else
        {
            GameObject freeSlotForArepas = GetFreeSlot();
            freeSlotForArepas.GetComponent<MesaFood>().AddFoodToMesa();
            Debug.Log("The arepas is added to mesa");
        }

        return freeSlot;
    }

    private bool CheckFreeSlot()
    {

        foreach (GameObject mesaFood in mesaFoodList)
        {
            if (mesaFood.GetComponent<MesaFood>().isEmpty)
            {
                return true;
            }
        }
        return false;
    }

    private GameObject GetFreeSlot()
    {

        foreach (GameObject mesaFood in mesaFoodList)
        {
            if (mesaFood.GetComponent<MesaFood>().isEmpty)
            {
                return mesaFood;
            }
        }
        Debug.LogError("Something went wrong on GetFreeSlotmesafood");
        return null;
    }

}
