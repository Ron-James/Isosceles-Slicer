using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadMenu(){
        SceneManager.LoadScene(0);
        GameInputEventSystem.instance.StopRumble();
    }
    public void LoadGame(){
        SceneManager.LoadScene(1);
        GameInputEventSystem.instance.StopRumble();
    }
    public void QuitGame(){
        GameInputEventSystem.instance.StopRumble();
        Application.Quit();
    }
}
