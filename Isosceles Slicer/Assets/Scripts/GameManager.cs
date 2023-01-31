using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton_Code

    public static GameManager instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion
    
    [SerializeField] float _rootVulnerableTime = 10;

    public float RootVulnerableTime { get => _rootVulnerableTime; set => _rootVulnerableTime = value; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

}
