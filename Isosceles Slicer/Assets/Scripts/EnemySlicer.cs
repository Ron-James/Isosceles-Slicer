using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySlicer : MonoBehaviour
{
    [Header("Slice Variable")]
    [SerializeField] Rigidbody2D _rightHalf;
    [SerializeField] Rigidbody2D _leftHalf;
    [SerializeField] float _breakForce = 10f;
    [SerializeField] float _gravityScale = 1;

    [SerializeField] Sound _sliceSound;

    EnemyEvents _enemyEvents;
    private void Awake()
    {
        _enemyEvents = GetComponent<EnemyEvents>();
        _enemyEvents.onEnemyDeath += OnEnemyDeath;
        _enemyEvents.onEnemyEnable += OnEnemyEnable;
        _enemyEvents.onEnemyDisable += OnEnemyDisable;
    }
    // Start is called before the first frame update
    void Start()
    {
        _sliceSound.src = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            StartCoroutine(Slice(20));
        }
    }

    public void SliceEnemy()
    {
        StartCoroutine(Slice(5));
    }

    public void SetStatic()
    {
        _leftHalf.bodyType = RigidbodyType2D.Static;
        _rightHalf.bodyType = RigidbodyType2D.Static;

        _rightHalf.gravityScale = 0;
        _leftHalf.gravityScale = 0;
    }
    public void SetDynamic()
    {
        _leftHalf.bodyType = RigidbodyType2D.Dynamic;
        _rightHalf.bodyType = RigidbodyType2D.Dynamic;

        _rightHalf.gravityScale = _gravityScale;
        _leftHalf.gravityScale = _gravityScale;
    }

    public void ResetEnemyHalves(){
        SetStatic();
        
        //_rightHalf.MovePosition(_rightHalf.transform.TransformPoint(Vector3.zero));
        //_leftHalf.MovePosition(_leftHalf.transform.TransformPoint(Vector3.zero));
        _rightHalf.transform.localPosition = Vector3.zero;
        _leftHalf.transform.localPosition = Vector3.zero;

        _rightHalf.transform.localEulerAngles = Vector3.zero;
        _leftHalf.transform.localEulerAngles = Vector3.zero;
    }
    private void OnEnemyEnable(RootController targetRoot)
    {
        SetStatic();
        ResetEnemyHalves();
    }
    private void OnEnemyDeath()
    {

    }

    private void OnEnemyDisable()
    {
        //SetStatic();
        //ResetEnemyHalves();
    }

    IEnumerator Slice(float duration)
    {
        _sliceSound.PlayOnce();
        SetDynamic();
        _leftHalf.gravityScale = _gravityScale;
        _rightHalf.gravityScale = _gravityScale;

        _rightHalf.AddForce(transform.right * _breakForce);
        _leftHalf.AddForce(-transform.right * _breakForce);
        float time = 0;
        _enemyEvents.EnemyDeath();
        while (true)
        {
            if (time >= duration)
            {
                ResetEnemyHalves();
                _enemyEvents.EnemyDisable();
                break;
            }
            else
            {
                time += Time.deltaTime;
                yield return null;
            }
        }
    }
}
