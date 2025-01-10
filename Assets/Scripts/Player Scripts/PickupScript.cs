using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupScript : MonoBehaviour
{
    public int meat, wood, stone;
    [SerializeField] FoodContainer foodContainer;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Meat")
        {
            meat++;
            foodContainer.RefillFood(5);
            other.gameObject.SetActive(false);
        }
        
        if (other.gameObject.tag == "Wood")
        {
            wood++;
            other.gameObject.SetActive(false);
        }

        if (other.gameObject.tag == "Stone")
        {
            stone++;
            other.gameObject.SetActive(false);
        }


    }
}
