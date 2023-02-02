using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyController : MonoBehaviour
{
    [SerializeField] Transform[] _segments;
    [SerializeField] GameObject _segmentsContainer;
    [SerializeField] float _damage = 5;
    [SerializeField] RootController _targetRoot;


    [Header("Slice Variable")]
    [SerializeField] Rigidbody2D _rightHalf; 
    [SerializeField] Rigidbody2D _leftHalf;
    [SerializeField] float _breakForce = 10f;
    [SerializeField] float _gravityScale = 1;
    EnemyEvents _enemyEvents;

    Collider2D _collider;



    

    public RootController TargetRoot { get => _targetRoot; set => _targetRoot = value; }
    private void Awake() {
        _enemyEvents = GetComponent<EnemyEvents>();
        _collider = GetComponent<Collider2D>();
        
        _segments = _segmentsContainer.GetComponentsInChildren<Transform>();
        if (_damage > 100)
        {
            _damage = 100;
        }

        _enemyEvents.onEnemyEnable += OnEnemyEnable;
        _enemyEvents.onEnemyDisable += OnEnemyDisable;
        _enemyEvents.onEnemyDeath += OnEnemyDeath;
    }
    // Start is called before the first frame update
    void Start()
    {
        
        _enemyEvents.EnemyDisable();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnEnemyEnable(RootController targetRoot){
        this._targetRoot = targetRoot;
        _collider.enabled = true;
        SetSegmentSpriteActive(true);
    }

    private void OnEnemyDeath(){
        _collider.enabled = false;
        transform.SetParent(EnemySpawner.instance.InactiveContainer.transform);
    }
    private void OnEnemyDisable(){
        transform.SetParent(EnemySpawner.instance.InactiveContainer.transform);
        _collider.enabled = false;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
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
                if (rootController == _targetRoot && !rootController.IsVulnerable && _enemyEvents.IsActive)
                {
                    float damagePercent = _damage / 100;
                    float currentRootGrowth = _targetRoot.CurrentRootLength();
                    float damagedRootGrowth = currentRootGrowth - damagePercent;
                    _targetRoot.ChangeSize(damagedRootGrowth);
                }
                _enemyEvents.EnemyDisable();
                break;
            case "Core":
                if(_targetRoot.IsVulnerable){
                    if(_enemyEvents.IsActive){
                        Debug.Log("Game Over");
                        GameManager.instance.GameOver();
                        _enemyEvents.EnemyDisable();
                    }
                    
                }
                else{
                    float damagePercent = _damage / 100;
                    float currentRootGrowth = _targetRoot.CurrentRootLength();
                    float damagedRootGrowth = currentRootGrowth - damagePercent;
                    _targetRoot.ChangeSize(damagedRootGrowth);
                }
            break;
        }
    }

    

    
    

    

    
}
