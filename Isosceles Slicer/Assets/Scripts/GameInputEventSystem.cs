using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public enum MovementState
{
    stationary = 0,
    moving = 1,
    dashing = 2
}
public class GameInputEventSystem : MonoBehaviour
{
    Gamepad _gamepad;
    Coroutine controllerRumble;
    [SerializeField] MovementState _movementState;
    #region Singleton_Code
    public static GameInputEventSystem instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion

    #region Dash_Events
    public event Action onDashEnter;
    public event Action onDashExit;
    public void PlayerDashEnter()
    {
        if (onDashEnter != null)
        {

            _movementState = MovementState.dashing;
            onDashEnter();
        }
    }
    public void PlayerDashExit()
    {
        if (onDashExit != null)
        {
            if (moveDirection.magnitude > 0)
            {
                _movementState = MovementState.moving;
                Debug.Log("Is still moving");
            }
            else
            {
                Debug.Log("Is not still moving");
                _movementState = MovementState.stationary;
            }

            onDashExit();
        }
    }

    #endregion

    #region Movement_Events
    public static Vector3 moveDirection;
    [SerializeField] Vector3 currentDirection;
    public static bool isMoving;

    public MovementState MovementState { get => _movementState; set => _movementState = value; }

    public event Action onMovementEnter;
    public event Action onMovementExit;

    public void MovementEnter()
    {
        if (onMovementEnter != null)
        {
            onMovementEnter();
        }
    }

    public void MovementExit()
    {
        if (onMovementExit != null)
        {
            onMovementExit();
        }
    }
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        isMoving = false;
        moveDirection = Vector3.zero;
    }

    public void StopRumble(){
        if(_gamepad != null){
            _gamepad.SetMotorSpeeds(0, 0);
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        currentDirection = moveDirection;
        if ((Input.GetButtonDown("Submit") || Input.GetMouseButtonDown(0)) && _movementState != MovementState.dashing)
        {
            instance.PlayerDashEnter();
        }

        MovementInfo();



    }

    public void MovementInfo()
    {
        float xMovement = Input.GetAxisRaw("Horizontal");
        float yMovement = Input.GetAxisRaw("Vertical");

        if (_movementState == MovementState.dashing)
        {
            return;
        }
        else
        {
            moveDirection = new Vector3(xMovement, yMovement, 0).normalized;
        }


        if (moveDirection.magnitude > 0 && _movementState == MovementState.stationary)
        {
            instance.MovementEnter();
            _movementState = MovementState.moving;
        }
        else if (moveDirection.magnitude < 0.001f && _movementState == MovementState.moving)
        {
            instance.MovementExit();
            _movementState = MovementState.stationary;
        }

    }
    public void RumbleController(float duration)
    {
        controllerRumble = StartCoroutine(RumbleControllerRoutine(duration, 0.25f, 0.75f));
    }
    IEnumerator RumbleControllerRoutine(float duration, float lowFrequency, float highFrequency)
    {
        float time = 0;
        _gamepad = Gamepad.current;
        if (_gamepad != null)
        {
            _gamepad.SetMotorSpeeds(lowFrequency, highFrequency);
        }
        while (true)
        {
            if (time >= duration)
            {
                if (_gamepad != null)
                {
                    _gamepad.SetMotorSpeeds(0, 0);
                }
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
