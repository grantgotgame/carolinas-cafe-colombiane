using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class CustomerSpawnManager : MonoBehaviour
{
    public static CustomerSpawnManager SharedInstance;
    public List<Customer> pooledObjects;
    public GameObject objectToPool;
    public int amountToPool = 20;


    private void Start()
    {
        
    }

}
