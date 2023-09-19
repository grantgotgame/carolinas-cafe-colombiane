using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CustomerSO : ScriptableObject
{
    //I can create one type of customer at the time, after he goes away, we just disable it and when he appear again, we already recorded the happiness bar in the class Customer
    public int univoqueIdCustomer;
    public int amountValueHappynessBar;
    public string customerName;
    public Sprite customerImage;
    //TODO add dialogue script here


}
