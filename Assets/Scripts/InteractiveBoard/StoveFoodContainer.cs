using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveFoodContainer : MonoBehaviour
{

    public List<GameObject> stoveFoodList = new List<GameObject>();
    [SerializeField] MesaFoodContainer mesaFoodContainer;

    private void Start()
    {
        foreach (Transform stoveFood in gameObject.transform)
        {
            stoveFood.gameObject.AddComponent<StoveFood>();
            stoveFoodList.Add(stoveFood.gameObject);
        }
    }

    public bool AddFoodInMesaContainer(bool isPrepared)
    {
        if (!isPrepared)
        {
            Debug.Log("The food is not ready");
            return false;
        }
        else
        {
            //TODO add food in the mesa
            freeSlot = mesaFoodContainer.CreateArepas();
            Debug.Log("We are trying to add food in the mesa");
            return freeSlot;
        }
    }

    bool freeSlot;

    public void CreateArepas()
    {
        freeSlot = CheckFreeSlot();
        if (!freeSlot)
        {
            Debug.Log("stove food container is full");
        }
        else
        {
            GameObject freeSlotForArepas = GetFreeSlot();
            freeSlotForArepas.GetComponent<StoveFood>().CreateArepas();
         //   freeSlotForArepas.GetComponent<StoveFood>().isEmpty = false;
            Debug.Log("The arepas is added to stove");
        }
    }

    private bool CheckFreeSlot()
    {

        foreach (GameObject stoveFood in  stoveFoodList) {
            if (stoveFood.GetComponent<StoveFood>().isEmpty)
            {
                return true;
            }
        } 
            return false;
    }

    private GameObject GetFreeSlot()
    {

        foreach (GameObject stoveFood in  stoveFoodList) {
            if (stoveFood.GetComponent<StoveFood>().isEmpty)
            {
                return stoveFood;
            }
        }
        Debug.LogError("Something went wrong on AddArepasToFreeSlot");
            return null;
    }
}
