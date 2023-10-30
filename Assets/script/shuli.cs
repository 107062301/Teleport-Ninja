using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shuli : MonoBehaviour
{
    // Start is called before the first frame update
    float life = 30.0f;
    public bool onwall = false, iskill = false;
    public Sprite shuli2;
    GameObject PLAYER;
    Sprite origin;
    void Start()
    {
        origin = GetComponent<SpriteRenderer>().sprite;
        PLAYER = GameObject.Find("pplayer/player");
    }

    // Update is called once per frame
    void Update()
    {
        life -= Time.deltaTime;
        if (life <= 0) Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "shuli" && !onwall)
        {
            GetComponent<SpriteRenderer>().sprite = shuli2;
            Vector2 v = GetComponent<Rigidbody2D>().velocity, cov = collision.GetComponent<Rigidbody2D>().velocity;
            Vector2 cv = collision.gameObject.transform.position, pv = transform.position;
            if (Mathf.Abs(v.x) > Mathf.Abs(v.y))  
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(-v.x, v.y + 4);
                Debug.Log("1");
            }
            else if (Mathf.Abs(v.x) < Mathf.Abs(v.y) && v.y > 0)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(v.x, -v.y - 4);
                Debug.Log("2");
            }
            else if (Mathf.Abs(v.x) < Mathf.Abs(v.y) && v.y < 0)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(v.x, -v.y + 4);
                Debug.Log("3");
            }
            else
            {
                GetComponent<Rigidbody2D>().velocity = -v;
            }
           
        }

        else if (collision.gameObject.tag == "wall")
        {
            GetComponent<SpriteRenderer>().sprite = origin;
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            GetComponent<Rigidbody2D>().angularVelocity = 0;
            GetComponent<Rigidbody2D>().gravityScale = 0.0f;
           
            
            /*Vector2 v = GetComponent<Rigidbody2D>().velocity;
            GetComponent<Rigidbody2D>().velocity = Vector2.Reflect(v,Vector2.right);*/
           
            onwall = true;
        }
        else if (collision.gameObject.tag == "enemy" && !iskill)
        {
            if (!collision.GetComponent<enemy1>().die && !onwall)
            {
                transform.position = new Vector3(collision.transform.position.x, transform.position.y, transform.position.z);
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 7);
                iskill = true;
                GetComponent<SpriteRenderer>().sprite = shuli2;
                PLAYER.GetComponent<player>().slash = true;
                PLAYER.GetComponent<player>().px = transform.position.x;
                PLAYER.GetComponent<player>().py = transform.position.y;
            }

        }
        
        Debug.Log("shulican");
    }
    
    
    

}
