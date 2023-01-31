using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyController : MonoBehaviour
{
    [SerializeField] Transform[] _segments;
    [SerializeField] GameObject _segmentsContainer;
    [SerializeField] float damage = 5;
    [SerializeField] RootController targetRoot;
    EnemyEvents _enemyEvents;
    Collider2D _collider;

    

    public RootController TargetRoot { get => targetRoot; set => targetRoot = value; }
    private void Awake() {
        _enemyEvents = GetComponent<EnemyEvents>();
    }
    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<Collider2D>();
        
        
        _segments = _segmentsContainer.GetComponentsInChildren<Transform>();
        if (damage > 100)
        {
            damage = 100;
        }

        _enemyEvents.onEnemyEnable += OnEnemyEnable;
        _enemyEvents.onEnemyDisable += OnEnemyDisable;
        //EnemyDisable();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnEnemyEnable(){
        _collider.enabled = true;
        SetSegmentSpriteActive(true);
    }
    private void OnEnemyDisable(){
        _collider.enabled = false;
        SetSegmentSpriteActive(false);
    }
    public void SetSegmentSpriteActive(bool active){
        foreach(Transform segment in _segments){
            SpriteRenderer spriteRenderer = segment.GetComponent<SpriteRenderer>();
            if(spriteRenderer != null){
                spriteRenderer.enabled = active;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "Root":
                Debug.Log("connected with root");
                RootController rootController = other.gameObject.GetComponentInParent<RootController>();
                if (rootController == targetRoot && !rootController.IsVulnerable)
                {
                    float damagePercent = damage / 100;
                    float currentRootGrowth = rootController.CurrentRootLength();
                    float damagedRootGrowth = currentRootGrowth - damagePercent;
                    rootController.ChangeSize(damagedRootGrowth);
                }
                _enemyEvents.EnemyDisable();
                break;
            case "Core":
                if(targetRoot.IsVulnerable){
                    Debug.Log("Game Over");
                    _enemyEvents.EnemyDisable();
                }
            break;
        }
    }
    

    

    
}
