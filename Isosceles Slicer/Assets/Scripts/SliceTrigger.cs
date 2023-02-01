using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliceTrigger : MonoBehaviour
{
    PlayerController _playerController;
    // Start is called before the first frame update
    void Start()
    {
        _playerController = GetComponentInParent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(GameInputEventSystem.instance.MovementState == MovementState.dashing && other.tag == "Enemy"){
            Debug.Log("Sliced Enemy");
            EnemySlicer enemySlicer = other.gameObject.GetComponentInParent<EnemySlicer>();
            enemySlicer.SliceEnemy();
        }
    }
}
