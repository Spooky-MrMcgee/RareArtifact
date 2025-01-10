using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HorseCreature : BaseCreature
{
    GameObject[] trees;
    float closestTreeDistance;
    GameObject treeTask;
    Transform closestTree;

    

    public override void Running()
    {
        base.SwitchAnimationStates("isRunning");
        if ((Vector3.Distance(transform.position, closestTree.position) < nMA.stoppingDistance + 1) && base.currentState != StateMachine.DoTask)
        {
            Debug.Log(base.nMA.remainingDistance + " " + base.nMA.stoppingDistance);
            base.currentState = StateMachine.DoTask;
        }
    }

    public override void FindTask()
    {
        if (base.taskFound == false)
        {
            trees = GameObject.FindGameObjectsWithTag("Tree");
            closestTreeDistance = Vector3.Distance(transform.position, trees[0].transform.position);
            closestTree = trees[0].transform;
            foreach (GameObject tree in trees)
            {
                if (Vector3.Distance(transform.position, tree.transform.position) <= closestTreeDistance)
                {
                    closestTree = tree.transform;
                    closestTreeDistance = Vector3.Distance(transform.position, closestTree.position);
                    //Debug.Log("Closest tree is" + closestTree.name);
                }
            }
            base.nMA.SetDestination(closestTree.position);
            treeTask = closestTree.gameObject;
            base.taskFound = true;
        }
    }

    public override void DoTask()
    {
        CollectableObjects treeCollectable = treeTask.GetComponent<CollectableObjects>(); 
        if (treeCollectable.isBroken == true)
        {
            treeTask.gameObject.SetActive(false);
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
                treeCollectable.DamageDurability(base.taskDamage);
                hunger -= treeCollectable.hungerCost;
                taskTimer = 0f;
            }
        }
    }
}
