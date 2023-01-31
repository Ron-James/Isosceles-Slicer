using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavigation : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float _speed = 3.5f;
    EnemyController _enemyController;
    NavMeshAgent _agent;
    EnemyEvents _enemyEvents;
    private void Awake() {
        _enemyEvents = GetComponent<EnemyEvents>();
        _enemyEvents.onEnemyEnable += OnEnemyEnable;
    }
    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _enemyController = GetComponent<EnemyController>();
        
        //agent.UpdateAxis = false;
    }

    private void OnEnemyEnable() {
        _agent.speed = _speed;
        RootController root = GameManager.instance.GetRandomRoot();
        _enemyController.TargetRoot = root;
        if(!root.IsVulnerable){
            target = root.transform;
        }
        else{
            target = GameManager.instance.Core.transform;
        }
        _agent.SetDestination(target.position); 
    }

    private void onEnemyDisable(){
        _agent.speed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
              
        transform.rotation = Quaternion.Euler(Vector3.zero);
    }
}
