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
    [SerializeField] GameObject _rootsContainer;
    [SerializeField] RootController[] _roots = new RootController[4];
    [SerializeField] GameObject _core;

    [SerializeField] GameObject _victoryScreen;
    [SerializeField] GameObject _gameOverScreen;



    public float RootVulnerableTime { get => _rootVulnerableTime; set => _rootVulnerableTime = value; }
    public RootController[] Roots { get => _roots; set => _roots = value; }
    public GameObject Core { get => _core; set => _core = value; }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        _roots = _rootsContainer.GetComponentsInChildren<RootController>();
        _victoryScreen.SetActive(false);
        _gameOverScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public RootController GetRandomRoot()
    {
        int random = Random.Range(0, _roots.Length);
        return _roots[random];
    }

    public void Victory()
    {
        Time.timeScale = 0;
        _victoryScreen.SetActive(true);
        _gameOverScreen.SetActive(false);
        
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        _victoryScreen.SetActive(false);
        _gameOverScreen.SetActive(true);
    }


    public RootController GetClosestRoot(Vector3 position)
    {
        RootController closestRoot = _roots[0];
        float smallestDistance = Vector3.Distance(position, closestRoot.transform.position);
        foreach (RootController item in _roots)
        {
            float distance = Vector3.Distance(position, item.transform.position);
            if (distance < smallestDistance)
            {
                smallestDistance = distance;
                closestRoot = item;
            }

        }
        return closestRoot;
    }



}
