using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; 
public class WanderingMovement : MonoBehaviour
{
    [SerializeField] private NavMeshAgent nMA;
    [SerializeField] private float range;
    [SerializeField] private Transform centrePoint;
    Vector3 randomPoint, point;

    // Start is called before the first frame update
    void Start()
    {
        nMA = gameObject.GetComponent<NavMeshAgent>();
        centrePoint = gameObject.transform;
    }

    public void AssignWanderingPath()
    {
        if (nMA.remainingDistance <= nMA.stoppingDistance)
        {
            if (PickWanderingPath(centrePoint.position, range, out point))
            {
                nMA.SetDestination(point);
            }
        }
    }

    bool PickWanderingPath(Vector3 sphereCenter, float sphereRange, out Vector3 result)
    {
        randomPoint = sphereCenter + Random.insideUnitSphere * range;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }
}
