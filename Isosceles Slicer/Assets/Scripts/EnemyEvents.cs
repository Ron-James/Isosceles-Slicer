using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyEvents : MonoBehaviour
{
    [SerializeField] bool isActive;

    public bool IsActive { get => isActive; set => isActive = value; }
    #region Events
    public event Action onEnemyEnable;
    public event Action onEnemyDisable;
    public void EnemyEnable()
    {
        if (onEnemyEnable != null)
        {
            isActive = true;
            onEnemyEnable();
        }
    }
    public void EnemyDisable()
    {
        if (onEnemyEnable != null)
        {
            isActive = false;
            onEnemyDisable();
        }
    }
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
}
