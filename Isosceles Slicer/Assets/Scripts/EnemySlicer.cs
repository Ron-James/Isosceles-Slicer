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

    EnemyEvents _enemyEvents;
    private void Awake() {
        _enemyEvents = GetComponent<EnemyEvents>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.S)){
            StartCoroutine(Slice(20));
        }
    }

    public void SliceEnemy(){
        StartCoroutine(Slice(20));
    }
    IEnumerator Slice(float duration){
        _rightHalf.simulated = true;
        _leftHalf.simulated = true;
        _leftHalf.gravityScale = _gravityScale;
        _rightHalf.gravityScale = _gravityScale;

        _rightHalf.AddForce(transform.right * _breakForce);
        _leftHalf.AddForce(-transform.right * _breakForce);
        float time = 0;

        while(true){
            if(time >= duration){
                _rightHalf.simulated = false;
                _leftHalf.simulated = false;
                _leftHalf.gravityScale = 0;
                _rightHalf.gravityScale = 0;
                _rightHalf.transform.localPosition = Vector3.zero;
                _leftHalf.transform.localPosition = Vector3.zero;
                _enemyEvents.EnemyDisable();
                break;
            }
            else{
                time += Time.deltaTime;
                yield return null;
            }
        }
    }
}
