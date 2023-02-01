using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SImpleEnemyMovement : MonoBehaviour
{
    [SerializeField] Transform target;
    NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        //agent.UpdateAxis = false;
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(target.position);
               
        transform.rotation = Quaternion.Euler(Vector3.zero);
    }
}
