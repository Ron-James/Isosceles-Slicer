using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliceTrigger : MonoBehaviour
{
    PlayerController _playerController;
    // Start is called before the first frame update
    private void Awake()
    {

    }
    void Start()
    {
        _playerController = GetComponentInParent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (GameInputEventSystem.instance.MovementState == MovementState.dashing && other.tag == "Enemy")
        {
            if (other.GetComponent<EnemyEvents>().IsActive)
            {
                Debug.Log("Sliced Enemy");
                EnemySlicer enemySlicer = other.gameObject.GetComponentInParent<EnemySlicer>();
                enemySlicer.SliceEnemy();
            }

        }
    }


    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Enemy" && GameInputEventSystem.instance.MovementState == MovementState.dashing)
        {
            if (other.GetComponent<EnemyEvents>().IsActive)
            {
                EnemySlicer enemySlicer = other.gameObject.GetComponentInParent<EnemySlicer>();
                enemySlicer.SliceEnemy();
            }
        }
    }
}
