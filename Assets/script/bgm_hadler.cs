using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class bgm_hadler : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip bgm1, bgm2, main;
    AudioSource As;
    void Start()
    {
        As = GetComponent<AudioSource>();
        if(SceneManager.GetActiveScene().name == "game1")
        {
            As.volume = 0.2f;
            As.clip = bgm1;
            As.Play();
        }
        else if (SceneManager.GetActiveScene().name == "game2")
        {
            As.volume = 0.2f;
            As.clip = bgm2;
            As.Play();
        }
        else if (SceneManager.GetActiveScene().name == "main")
        {
            As.volume = 0.4f;
            As.clip = main;
            As.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
