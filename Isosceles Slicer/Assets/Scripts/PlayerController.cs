using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Configuration")]
    [SerializeField] float _maximumMoveSpeed;
    [SerializeField] float _minimumMoveSpeed = 0;
    [SerializeField] float _speedUpDuration = 0.5f;
    [SerializeField] float _rotationSpeed = 1f;
    [SerializeField] AnimationCurve _speedUpCurve;

    [SerializeField] float _maxHorizontalMovement = 6;
    [SerializeField] float _maxVerticalMovement = 10;


    [Header("Dash Configuration")]
    [SerializeField] float _dashTime = 1;
    [SerializeField] float _dashDistance = 5;
    [SerializeField] AnimationCurve _dashCurve;
    [SerializeField] GameObject _dashColor;
    [SerializeField] Sound _dashSound;



    float _currentMoveSpeed;
    Vector3 _currentMoveDirection;


    Rigidbody2D _rigidbody;

    private void Awake() {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        GameInputEventSystem.instance.onDashEnter += onDashEnter;
        GameInputEventSystem.instance.onMovementEnter += onMovementEnter;
        GameInputEventSystem.instance.onMovementExit += onMovementExit;
        GameInputEventSystem.instance.onDashExit += onDashExit;
        _dashSound.src = GetComponent<AudioSource>();
    }


    private void OnDisable()
    {
        GameInputEventSystem.instance.onDashEnter -= onDashEnter;
        GameInputEventSystem.instance.onMovementEnter -= onMovementEnter;
        GameInputEventSystem.instance.onMovementExit -= onMovementExit;
    }

    private void onDashEnter()
    {
        Dash();
        Debug.Log("Dash Called");
    }
    private void onDashExit(){
        Debug.Log("Dash Exit");
    }

    private void onMovementEnter()
    {
        Debug.Log("Movemnt Enter");
        IncreaseSpeed();
    }

    private void onMovementExit()
    {
        Debug.Log("Movemnt Exit");
    }
    // Update is called once per frame
    void Update()
    {
        ApplyMovement();
        RotateInMovementDirection();
    }


    private void ApplyMovement()
    {
        Vector3 direction = GameInputEventSystem.moveDirection;
        float distance = Time.fixedDeltaTime * _currentMoveSpeed;
        Quaternion currentRotation = transform.rotation;
        if (distance > 0)
        {
            //Quaternion targetRotation = Quaternion.LookRotation(transform.position + direction.normalized, Vector3.forward);
            //transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            Vector3 movePosition = transform.position + (distance * direction);
            float verticalMovePosition = Mathf.Clamp(movePosition.y, -_maxVerticalMovement, _maxVerticalMovement);
            float horizontalMovePosition = Mathf.Clamp(movePosition.x, -_maxHorizontalMovement, _maxHorizontalMovement);
            movePosition = new Vector3(horizontalMovePosition, verticalMovePosition, 0); 
            _rigidbody.MovePosition(movePosition);
        }
        else
        {
            return;
        }
    }

    private void RotateInMovementDirection()
    {
        Vector3 direction = GameInputEventSystem.moveDirection;
        if (direction.magnitude > 0)
        {
            Quaternion targetRotation = Quaternion.LookRotation(transform.forward, direction.normalized);
            Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.fixedDeltaTime);
            _rigidbody.MoveRotation(rotation);
        }
    }

    #region Speed_Control
    public void IncreaseSpeed()
    {
        StartCoroutine(IncreaseSpeedRoutine(_speedUpDuration));
    }
    IEnumerator IncreaseSpeedRoutine(float duration)
    {
        float time = 0;

        while (true)
        {

            if (time >= duration)
            {
                _currentMoveSpeed = _maximumMoveSpeed;
                break;
            }
            else
            {
                time += Time.fixedDeltaTime;
                float ratio = time / duration;
                float speedDifference = _maximumMoveSpeed - _minimumMoveSpeed;
                _currentMoveSpeed = _minimumMoveSpeed + (_speedUpCurve.Evaluate(ratio) * speedDifference);
                yield return new WaitForFixedUpdate();
            }

        }
    }
    #endregion
    
    private void OnDrawGizmos() {
        Gizmos.DrawLine(transform.position, transform.position + GameInputEventSystem.moveDirection);
    }

    public void Dash(){
        StartCoroutine(DashRoutine(_dashTime, _dashDistance));
    }
    IEnumerator DashRoutine(float duration, float distance){
        _dashSound.PlayOnce();
        float time = 0; 
        Vector3 initialPosition = transform.position;
        _dashColor.SetActive(true);
        while(true){
            if(time >= duration){
                _dashColor.SetActive(false);
                transform.position = initialPosition + (transform.up * _dashDistance);
                GameInputEventSystem.instance.PlayerDashExit();
                break;
            }
            else{
                time += Time.deltaTime;
                float ratio = time/duration;
                float currentDistance = _dashCurve.Evaluate(ratio) * distance;
                Vector3 movePosition = initialPosition + (transform.up * currentDistance);
                _rigidbody.MovePosition(movePosition);
                yield return null;
            }
        }
    }
    
}

