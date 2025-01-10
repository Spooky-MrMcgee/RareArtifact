using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UIHandler : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI foodText, woodText, stoneText;
    [SerializeField] FoodContainer foodContainer;
    [SerializeField] PickupScript pickupScript;
    // Update is called once per frame
    void Update()
    {
        foodText.text = "Food: " + foodContainer.currentFood;
        woodText.text = "Wood: " + pickupScript.wood;
        stoneText.text = "Stone " + pickupScript.stone;
    }
}
