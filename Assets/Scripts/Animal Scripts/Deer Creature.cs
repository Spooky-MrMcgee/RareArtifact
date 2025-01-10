using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeerCreature : BaseCreature
{
    GameObject[] rocks;
    float closestRockDistance;
    GameObject rockTask;
    Transform closestRock;

    

    public override void Running()
    {
        base.SwitchAnimationStates("isRunning");
        if ((Vector3.Distance(transform.position, closestRock.position) < nMA.stoppingDistance + 1) && base.currentState != StateMachine.DoTask)
        {
            Debug.Log(base.nMA.remainingDistance + " " + base.nMA.stoppingDistance);
            base.currentState = StateMachine.DoTask;
        }
    }

    public override void FindTask()
    {
        if (base.taskFound == false)
        {
            rocks = GameObject.FindGameObjectsWithTag("Rock");
            closestRockDistance = Vector3.Distance(transform.position, rocks[0].transform.position);
            closestRock = rocks[0].transform;
            foreach (GameObject rock in rocks)
            {
                if (Vector3.Distance(transform.position, rock.transform.position) <= closestRockDistance)
                {
                    closestRock = rock.transform;
                    closestRockDistance = Vector3.Distance(transform.position, closestRock.position);
                    //Debug.Log("Closest rock is" + closestrock.name);
                }
            }
            base.nMA.SetDestination(closestRock.position);
            rockTask = closestRock.gameObject;
            base.taskFound = true;
        }
    }

    public override void DoTask()
    {
        CollectableObjects rockCollectable = rockTask.GetComponent<CollectableObjects>(); 
        if (rockCollectable.isBroken == true)
        {
            rockTask.gameObject.SetActive(false);
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
                rockCollectable.DamageDurability(base.taskDamage);
                hunger -= rockCollectable.hungerCost;
                taskTimer = 0f;
            }
        }
    }
}
