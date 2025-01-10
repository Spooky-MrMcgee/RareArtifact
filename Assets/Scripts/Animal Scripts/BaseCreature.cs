using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseCreature : MonoBehaviour, ICreature, IInteractable
{

    [SerializeField] private float walkSpeed;
    [SerializeField] Animator animator;
    [SerializeField] private Transform returnPoint;
    [SerializeField] private FoodContainer foodContainer;
    private bool isAssignedTask = false;
    private float wanderingTimer, wanderingLimit;
    protected NavMeshAgent nMA;
    private WanderingMovement wanderingMovement;
    public bool taskFound = false, initialAssignment = true;
    public float taskSpeed, taskTimer, taskDamage, hunger, maxHunger;
    public enum StateMachine
    { 
        Idle,
        Walking,
        Running,
        StartTask,
        DoTask,
        Hungry,
        StopTask,
    }

    public StateMachine currentState;

    void Start()
    {
        currentState = StateMachine.Idle;
        wanderingLimit = Random.Range(0f, 10f);
        wanderingMovement = gameObject.GetComponent<WanderingMovement>();
        nMA = gameObject.GetComponent<NavMeshAgent>();
        nMA.speed = walkSpeed; 
    }

    void Update()
    {
        HandleStates();
        Interact();
    }

    void HandleStates()
    {
        if (currentState != StateMachine.Hungry)
        {
            if (hunger <= 0f)
            {
                currentState = StateMachine.Hungry;
            }

            switch (currentState)
            {
                case StateMachine.Idle:
                    Idle(); break;

                case StateMachine.Walking:
                    Walking(); break;

                case StateMachine.Running:
                    Running(); break;

                case StateMachine.StartTask:
                    StartTask(); break;

                case StateMachine.DoTask:
                    DoTask(); break;

                case StateMachine.StopTask:
                    StopTask(); break;
            }

            if ((nMA.remainingDistance < nMA.stoppingDistance) && taskFound && initialAssignment)
            {
                initialAssignment = false;
                Debug.Log(nMA.remainingDistance + " " + nMA.stoppingDistance);
                currentState = StateMachine.DoTask;
            }

            if (isAssignedTask == false)
            {
                wanderingTimer += Time.deltaTime;
                if (wanderingTimer >= wanderingLimit)
                {
                    if (currentState == StateMachine.Walking)
                    {
                        currentState = StateMachine.Idle;
                    }
                    else if (currentState == StateMachine.Idle)
                    {
                        currentState = StateMachine.Walking;
                    }
                    wanderingTimer = 0f;
                    wanderingLimit = Random.Range(5, 15f);
                }
            }
        }
        else
        {
            StopTask();
        }
    }

    protected void SwitchAnimationStates(string boolChange)
    {
        foreach (AnimatorControllerParameter parameter in animator.parameters)
        {
            if (parameter.name != boolChange && parameter.type == AnimatorControllerParameterType.Bool)
            {
                animator.SetBool(parameter.name, false);
            }
            else if (parameter.type == AnimatorControllerParameterType.Bool)
            {
                animator.SetBool(parameter.name, true);
            }
        }
    }

    public void Interact()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isAssignedTask = true;
            currentState = StateMachine.Running;
            FindTask();
        }
    }

    public void Walking()
    {
        SwitchAnimationStates("isWalking");
        wanderingMovement.AssignWanderingPath();
    }

    public virtual void Running()
    {
        SwitchAnimationStates("isRunning");
    }

    public void Idle()
    {
        SwitchAnimationStates("isIdle");
    }

    public void StopTask()
    {
        nMA.isStopped = false;
        nMA.SetDestination(returnPoint.position);
        if (nMA.remainingDistance <= nMA.stoppingDistance + 1)
        {
            Feed();
        }
    }

    public void Feed()
    {
        taskTimer += Time.deltaTime;
        if (taskTimer >= (taskSpeed * 5))
        {
            hunger = foodContainer.EatFood(maxHunger);
            taskTimer = 0f;
        }
        if (hunger >= maxHunger / 2)
        {
            currentState = StateMachine.Idle;
            isAssignedTask = false;
            initialAssignment = true;
            taskFound = false;
        }    
    }

    public virtual void FindTask()
    {
        
    }

    public virtual void StartTask()
    {
        
    }

    public virtual void DoTask()
    {

    }
}
