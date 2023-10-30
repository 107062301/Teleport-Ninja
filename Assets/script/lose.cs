using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class lose : MonoBehaviour
{
    // Start is called before the first frame update
    float time = 4;
    public AudioClip ac;
    void Start()
    {
        GetComponent<AudioSource>().PlayOneShot(ac);
    }

    // Update is called once per frame
    void Update()
    {
        
        time -= Time.deltaTime;
        if(time <= 0)
        {
            SceneManager.LoadScene("game1");
            
        }
    }
}
