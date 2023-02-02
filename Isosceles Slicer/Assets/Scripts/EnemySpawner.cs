using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    #region Singleton_Code

    public static EnemySpawner instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion
    [SerializeField] GameObject _inactiveContainer;
    [SerializeField] GameObject _activeContainer;
    [SerializeField] float _spawnDistance = 10f;
    [SerializeField] float _minSpawnRate = 3f;
    [SerializeField] float _maxSpawnRate = 0.3f;
    [SerializeField] float _currentSpawnRate = 3f;

    [SerializeField] float _spawnTimer = 0;
    public GameObject ActiveContainer { get => _activeContainer; set => _activeContainer = value; }
    public GameObject InactiveContainer { get => _inactiveContainer; set => _inactiveContainer = value; }


    // Start is called before the first frame update
    void Start()
    {
        _spawnTimer = 0;
        _currentSpawnRate = _minSpawnRate;
    }

    // Update is called once per frame
    void Update()
    {
        _spawnTimer += Time.deltaTime;
        
        if(_spawnTimer >= _currentSpawnRate){
            SpawnEnemy();
            _spawnTimer = 0;
        }
        
    }

    public void SpawnEnemy(){
        float angle = Random.Range(0, 2 * Mathf.PI);
        Vector3 position = new Vector3(_spawnDistance * Mathf.Cos(angle), _spawnDistance * Mathf.Sin(angle), 0);
        EnemyEvents enemy = _inactiveContainer.GetComponentsInChildren<EnemyEvents>()[0];
        //RootController targetRoot = GameManager.instance.GetRandomRoot();
        RootController targetRoot = GameManager.instance.GetClosestRoot(position);
        enemy.transform.position = position;
        enemy.transform.SetParent(_activeContainer.transform);
        enemy.EnemyEnable(targetRoot);
    }



}
