using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] Transform[] _segments;
    [SerializeField] GameObject _segmentsContainer;
    [SerializeField] float damage = 5;
    [SerializeField] RootController targetRoot;
    // Start is called before the first frame update
    void Start()
    {
        _segments = _segmentsContainer.GetComponentsInChildren<Transform>();
        if (damage > 100)
        {
            damage = 100;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(Vector3.zero);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "Root":
                Debug.Log("connected with root");
                RootController rootController = other.gameObject.GetComponentInParent<RootController>();
                if (rootController == targetRoot && !rootController.IsVulnerable)
                {
                    float damagePercent = damage / 100;
                    float currentRootGrowth = rootController.CurrentRootLength();
                    float damagedRootGrowth = currentRootGrowth - damagePercent;
                    rootController.ChangeSize(damagedRootGrowth);
                }

                break;
            case "Core":
                if(targetRoot.IsVulnerable){
                    Debug.Log("Game Over");
                }
            break;
        }
    }

    
}
