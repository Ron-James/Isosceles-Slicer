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
    [SerializeField] GameObject _pauseScreen;

    public static bool isPaused;

    [SerializeField] Sound _gameOverSound;
    [SerializeField] Sound _winSound;
    [SerializeField] MusicBox _musicBox;




    public float RootVulnerableTime { get => _rootVulnerableTime; set => _rootVulnerableTime = value; }
    public RootController[] Roots { get => _roots; set => _roots = value; }
    public GameObject Core { get => _core; set => _core = value; }

    // Start is called before the first frame update
    void Start()
    {
        Resume();
        Time.timeScale = 1;
        _roots = _rootsContainer.GetComponentsInChildren<RootController>();
        _victoryScreen.SetActive(false);
        _gameOverScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Cancel")){
            if(!isPaused){
                PauseGame();
            }
            else{
                Resume();
            }
           
        }
    }

    public RootController GetRandomRoot()
    {
        int random = Random.Range(0, _roots.Length);
        return _roots[random];
    }

    public void Victory()
    {
        GameInputEventSystem.instance.StopRumble();
        _musicBox.StopMusic();
        Time.timeScale = 0;
        _victoryScreen.SetActive(true);
        _gameOverScreen.SetActive(false);
        _winSound.src = GetComponent<AudioSource>();
        _winSound.PlayOnce();
    }
    

    public void GameOver()
    {
        GameInputEventSystem.instance.StopAllCoroutines();
        _musicBox.StopMusic();
        Time.timeScale = 0;
        _victoryScreen.SetActive(false);
        _gameOverScreen.SetActive(true);
        _gameOverSound.src = GetComponent<AudioSource>();
        _gameOverSound.PlayOnce();
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

    public RootController LongestRoot(){
        float longest = _roots[0].CurrentRootLength();
        RootController longestRoot = _roots[0];

        foreach(RootController item in _roots){
            if(item.CurrentRootLength() > longest){
                longest = item.CurrentRootLength();
                longestRoot = item;
            }
        }

        return longestRoot;
    }

    public RootController ShortestRoot(){
        float shortest = _roots[0].CurrentRootLength();
        RootController shortestRoot = _roots[0];

        foreach(RootController item in _roots){
            if(item.CurrentRootLength() < shortest){
                shortest = item.CurrentRootLength();
                shortestRoot = item;
            }
        }

        return shortestRoot;
    }

    public RootController GetTargetRoot(){
        int random = Random.Range(0, 3);
        RootController root;
        switch(random){
            default:
                root = GetRandomRoot();
            break;
            case 0:
                root = ShortestRoot();
            break;
            case 1:
                root = LongestRoot();
            break;
            case 2:
                root = GetRandomRoot();
            break;
        }

        return root;
    }

    public void PauseGame(){
        isPaused = true;
        Time.timeScale = 0;
        _pauseScreen.SetActive(true);

    }

    public void Resume(){
        isPaused = false;
        Time.timeScale = 1;
        _pauseScreen.SetActive(false);
    }



}
