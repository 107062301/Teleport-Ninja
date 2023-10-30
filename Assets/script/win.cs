using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class win : MonoBehaviour
{
    float time = 5;
    public AudioClip ac;
    void Start()
    {
        GetComponent<AudioSource>().PlayOneShot(ac);
    }

    // Update is called once per frame
    void Update()
    {

        time -= Time.deltaTime;
        if (time <= 0)
        {
            SceneManager.LoadScene("main");

        }
    }
}
