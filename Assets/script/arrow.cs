using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrow : MonoBehaviour
{
    float life = 5.0f;
   
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        life -= Time.deltaTime;
        if (life <= 0) Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
       if (collision.gameObject.tag == "wall" || collision.name == "pplayer" || collision.name == "player")
        {
            Destroy(gameObject);
        }

        Debug.Log("shulican");
    }
}
