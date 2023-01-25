using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStoreInventory : MonoBehaviour
{
    [SerializeField] private int currentNumberOfCoins;


    public void ObtainCoins(int amount)
    {
        currentNumberOfCoins += amount;
    }

    public bool PayCoins(int amount)
    {
        if (currentNumberOfCoins >= amount)
        {
            currentNumberOfCoins -= amount;
            return true;
        }
        return false;
    }
    public int CurrentNumberOfCoins { get => currentNumberOfCoins; }
}
