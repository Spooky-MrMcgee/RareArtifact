using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogCreature : BaseCreature
{
    GameObject[] foods;
    float closestFoodDistance;
    GameObject foodTask;
    Transform closestFood;



    public override void Running()
    {
        base.SwitchAnimationStates("isRunning");
        if ((Vector3.Distance(transform.position, closestFood.position) < nMA.stoppingDistance + 1) && base.currentState != StateMachine.DoTask)
        {
            Debug.Log(base.nMA.remainingDistance + " " + base.nMA.stoppingDistance);
            base.currentState = StateMachine.DoTask;
        }
    }

    public override void FindTask()
    {
        if (base.taskFound == false)
        {
            foods = GameObject.FindGameObjectsWithTag("Food");
            closestFoodDistance = Vector3.Distance(transform.position, foods[0].transform.position);
            closestFood = foods[0].transform;
            foreach (GameObject food in foods)
            {
                if (Vector3.Distance(transform.position, food.transform.position) <= closestFoodDistance)
                {
                    closestFood = food.transform;
                    closestFoodDistance = Vector3.Distance(transform.position, closestFood.position);
                    //Debug.Log("Closest food is" + closestfood.name);
                }
            }
            base.nMA.SetDestination(closestFood.position);
            foodTask = closestFood.gameObject;
            base.taskFound = true;
        }
    }

    public override void DoTask()
    {
        CollectableObjects foodCollectable = foodTask.GetComponent<CollectableObjects>();
        if (foodCollectable.isBroken == true)
        {
            foodTask.gameObject.SetActive(false);
            base.nMA.isStopped = false;
            base.taskFound = false;
            base.currentState = StateMachine.Running;
            FindTask();
        }
        else if (hunger >= 0f)
        {
            base.nMA.isStopped = true;
            base.taskTimer += Time.deltaTime;
            if (taskTimer >= taskSpeed)
            {
                foodCollectable.DamageDurability(base.taskDamage);
                hunger -= foodCollectable.hungerCost;
                taskTimer = 0f;
            }
        }
    }
}
