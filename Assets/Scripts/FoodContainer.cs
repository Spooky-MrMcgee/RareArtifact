using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodContainer : MonoBehaviour
{
    [SerializeField] public float currentFood, maxFood;

    public float EatFood(float foodTaken)
    {
        if (currentFood <= foodTaken)
        {
            float foodToReturn = currentFood;
            currentFood = 0;
            return foodToReturn;
            
        }
        else
        {
            currentFood -= foodTaken;
            return foodTaken;
        }
    }

    public void RefillFood(float foodHeld)
    {
        currentFood += foodHeld;
        if (currentFood > maxFood)
        {
            currentFood = maxFood;
        }
    }
}
