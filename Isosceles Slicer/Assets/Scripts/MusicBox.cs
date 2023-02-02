using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicBox : MonoBehaviour
{
    [SerializeField] Sound _music;
    // Start is called before the first frame update
    void Start()
    {
        _music.src = GetComponent<AudioSource>();
        _music.PlayLoop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StopMusic(){
        _music.StopSource();
    }
}
