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
    [SerializeField] AnimationCurve _spawnRateCurve;
    [SerializeField] float _spawnDistance = 10f;
    [SerializeField] float _minSpawnRate = 3f;
    [SerializeField] float _maxSpawnRate = 0.3f;
    [SerializeField] float _currentSpawnRate = 3f;

    [SerializeField] float _spawnTimer = 0;
    [SerializeField] float _difficultyCurveTime = 60;
    public GameObject ActiveContainer { get => _activeContainer; set => _activeContainer = value; }
    public GameObject InactiveContainer { get => _inactiveContainer; set => _inactiveContainer = value; }


    // Start is called before the first frame update
    void Start()
    {
        _spawnTimer = 0;
        _currentSpawnRate = _minSpawnRate;
        StartCoroutine(IncreaseSpawnRateRoutine(_difficultyCurveTime));
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
        float angle = Random.Range(0, 45);
        //Vector3 position = new Vector3(_spawnDistance * Mathf.Cos(angle), _spawnDistance * Mathf.Sin(angle), 0);
        EnemyEvents enemy = _inactiveContainer.GetComponentsInChildren<EnemyEvents>()[0];
        //RootController targetRoot = GameManager.instance.GetRandomRoot();
        RootController targetRoot = GameManager.instance.GetTargetRoot();
        Vector3 position = targetRoot.transform.position + (targetRoot.transform.up * _spawnDistance);
        position = Quaternion.AngleAxis(angle, Vector3.forward) * position;
        enemy.transform.position = position;
        enemy.transform.SetParent(_activeContainer.transform);
        enemy.EnemyEnable(targetRoot);
    }

    IEnumerator IncreaseSpawnRateRoutine(float duration){
        float time = 0;
        float difference = _minSpawnRate - _maxSpawnRate;
        while(true){
            if(time >= duration){
                _currentSpawnRate = _maxSpawnRate;
                break;
            }
            else{
                time += Time.deltaTime;
                float ratio = time/duration;
                
                float current = _minSpawnRate - (_spawnRateCurve.Evaluate(ratio) * difference);
                _currentSpawnRate = current;
                yield return null;
            }
        }
    }


}
