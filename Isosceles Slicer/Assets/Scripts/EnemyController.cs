using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] Transform [] _segments;
    [SerializeField] GameObject _segmentsContainer;
    // Start is called before the first frame update
    void Start()
    {
        _segments = _segmentsContainer.GetComponentsInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
