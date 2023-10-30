using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class rst_obj : MonoBehaviour
{
    // Start is called before the first frame update
    SpriteRenderer sp;
    RawImage ri;
    public Text isreset;
    bool rst = false;
    public bool destroyed = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isreset.text == "true" && !rst)
        {
            rst = true;
            destroyed = false;
            if (gameObject.name == "sdoor"  || gameObject.name == "break" || gameObject.name == "door")
            {
                ri = gameObject.GetComponent<RawImage>();
                if(gameObject.name != "break") ri.color = new Color(ri.color.r, ri.color.g, ri.color.b, 1);
                else ri.color = new Color(ri.color.r, ri.color.g, ri.color.b, 0.35f);
                gameObject.GetComponent<BoxCollider2D>().enabled = true;
            }
            else
            {
                sp = gameObject.GetComponent<SpriteRenderer>();
                sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, 1);
            }
            

        }
        if (isreset.text == "false" && rst)
        {
            rst = false;
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "player")
        {
            sp = gameObject.GetComponent<SpriteRenderer>();
            sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, 0);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "player")
        {
           
            
            if (gameObject.name == "sdoor")
            {
                int skey = collision.gameObject.GetComponent<player>().skey;
                if(skey == 3)
                {
                    collision.gameObject.GetComponent<player>().skey = 0;
                    ri = gameObject.GetComponent<RawImage>();
                    ri.color = new Color(ri.color.r, ri.color.g, ri.color.b, 0);
                    gameObject.GetComponent<BoxCollider2D>().enabled = false;
                }
            }
            else if (gameObject.name == "door")
            {
                Debug.Log("door");
                bool key_doc = collision.gameObject.GetComponent<player>().has_key;
               
                if (key_doc)
                {
                    collision.gameObject.GetComponent<player>().has_key = false;
                    ri = gameObject.GetComponent<RawImage>();
                    ri.color = new Color(ri.color.r, ri.color.g, ri.color.b, 0);
                    gameObject.GetComponent<BoxCollider2D>().enabled = false;
                }
            }
            else if (gameObject.name == "break")
            {
                ri = gameObject.GetComponent<RawImage>();
                ri.color = new Color(ri.color.r, ri.color.g, ri.color.b, 0);
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
            }
            
        }
    }


}
