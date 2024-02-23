using UnityEngine;
using UnityEngine.AI;
public class Enemy : MonoBehaviour
{
    public NavMeshAgent pathfinder;
    private Transform target;
    
    void Start()
    {
        pathfinder = GetComponent<NavMeshAgent>();
        target = GameObject.Find("Player").transform;
    }

    void Update()
    {
        if (target != null)
        {
            pathfinder.SetDestination(target.position);
           // Debug.Log(target.position);
        }
    }
}