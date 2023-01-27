using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum MovementState{
    stationary = 0,
    moving = 1,
    dashing = 2
}
public class GameInputEventSystem : MonoBehaviour
{
    [SerializeField] MovementState _movementState;
    #region Singleton_Code
    public static GameInputEventSystem instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion

    #region Dash_Events
    public static bool isDashing;
    public event Action onDashEnter;
    public event Action onDashExit;
    public void PlayerDashEnter()
    {
        if (onDashEnter != null)
        {
            isDashing = true;
            _movementState = MovementState.dashing;
            onDashEnter();
        }
    }
    public void PlayerDashExit()
    {
        if (onDashExit != null)
        {
            if(moveDirection.magnitude > 0){
                _movementState = MovementState.moving;
                Debug.Log("Is still moving");
            }
            else{
                Debug.Log("Is not still moving");
                _movementState = MovementState.stationary;
            }
            isDashing = false;
            onDashExit();
        }
    }

    #endregion

    #region Movement_Events
    public static Vector3 moveDirection;
    public static bool isMoving;

    public MovementState MovementState { get => _movementState; set => _movementState = value; }

    public event Action onMovementEnter;
    public event Action onMovementExit;

    public void MovementEnter(){
        if(onMovementEnter != null){
            onMovementEnter();
        }
    }

    public void MovementExit(){
        if(onMovementExit != null){
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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _movementState != MovementState.dashing)
        {
            instance.PlayerDashEnter();
        }

        MovementInfo();

        

    }

    public void MovementInfo(){
        float xMovement = Input.GetAxisRaw("Horizontal");
        float yMovement = Input.GetAxisRaw("Vertical");

        if(_movementState == MovementState.dashing){
            return;
        }
        else{
            moveDirection = new Vector3(xMovement, yMovement, 0);
            moveDirection = moveDirection.normalized;
        }
        

        if(moveDirection.magnitude > 0 && _movementState == MovementState.stationary){
            instance.MovementEnter();
            _movementState = MovementState.moving;
        }
        else if(moveDirection.magnitude < 0.001f && _movementState == MovementState.moving){
            instance.MovementExit();
            _movementState = MovementState.stationary;
        }
        
    }
}
