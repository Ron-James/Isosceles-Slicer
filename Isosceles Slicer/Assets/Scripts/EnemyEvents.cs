using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyEvents : MonoBehaviour
{
    [SerializeField] bool isActive;

    public bool IsActive { get => isActive; set => isActive = value; }
    #region Events
    public event Action<RootController> onEnemyEnable;
    public event Action onEnemyDisable;
    public event Action onEnemyDeath;

    public void EnemyDeath(){
        if(onEnemyDeath != null){
            isActive = false;
            onEnemyDeath();
        }
    }
    public void EnemyEnable(RootController targetRoot)
    {
        if (onEnemyEnable != null)
        {
            isActive = true;
            onEnemyEnable(targetRoot);
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
