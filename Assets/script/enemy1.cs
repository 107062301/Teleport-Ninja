using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemy1 : MonoBehaviour
{
    Animator anim;
    public GameObject player_col,range;
    public Transform eenemy;
    public Rigidbody2D arrow;
    public Text isreset, kill_count;
    public AudioClip hurt, shoot;
    AudioSource AS;
    Vector3 pos;
    float clk = 5.0f, clk2 = 1.0f, clk3 = 0.56f;
    bool run = false, reset = false;
    public bool die = false;
    int shoot_r = 1, orign_dir;
    public int dir = 1;
   
    void Start()
    {
        anim = GetComponent<Animator>();
        AS = GetComponent<AudioSource>();
        pos = transform.position;
        orign_dir = dir;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        //transform.position += new Vector3(-1, 0, 0) * 10 * Time.deltaTime;
        if (!die)
        {
            if (Vector2.Distance(player_col.transform.position, transform.position) < 4.5 &&  Mathf.Abs(player_col.transform.position.y - transform.position.y) < 1 )
            {

                if(!anim.GetBool("run")) anim.SetBool("run", false);
                if(!anim.GetBool("atk")) anim.SetBool("atk", true);
                if (player_col.transform.position.x > transform.position.x  && Mathf.Abs(player_col.transform.position.x - transform.position.x) >= 0.5f )
                {
                  
                    eenemy.localScale = new Vector3(1, 1, 1);
                    shoot_r = 1;
                    Debug.Log("1w");
                }
                else if(player_col.transform.position.x < transform.position.x && Mathf.Abs(player_col.transform.position.x - transform.position.x) >= 0.5f)
                {
                    
                    eenemy.localScale = new Vector3(-1, 1, 1);
                    shoot_r = -1;
                }
                else
                {
                    //eenemy.localScale = new Vector3(1, 1, 1);
                    shoot_r = 1;
                    Debug.Log("2w");
                }

                if(clk3 <= 0)
                {
                    clk3 = 0.56f;
                    AS.PlayOneShot(shoot);
                    Rigidbody2D a;
                    if (shoot_r == -1)
                    {
                        a = Instantiate(arrow, transform.position - new Vector3(0.1f, 0.35f, 0), Quaternion.identity);
                        a.transform.eulerAngles = new Vector3(0, 0, 90);
                    }
                    else
                    {
                        a = Instantiate(arrow, transform.position - new Vector3(-0.1f, 0.35f, 0), Quaternion.identity);
                        a.transform.eulerAngles = new Vector3(0, 0, -90);
                    }
                    
                    a.velocity = new Vector2(shoot_r*5,0);
                    a.gravityScale = 0;
                }
                clk3 -= Time.deltaTime;

            }
            else
            {
                if (anim.GetBool("atk")) anim.SetBool("atk", false);
                clk -= Time.deltaTime;
                if (clk >= 2.0f)
                {
                    if (!anim.GetBool("run")) anim.SetBool("run", true);

                    if (dir == 1)
                    {
                        eenemy.transform.position += new Vector3(1, 0, 0) * 1 * Time.deltaTime;
                        eenemy.localScale = new Vector3(1, 1, 1);
                    }
                    else
                    {
                        eenemy.transform.position += new Vector3(-1, 0, 0) * 1 * Time.deltaTime;
                        eenemy.localScale = new Vector3(-1, 1, 1);
                    }
                }
                else if (clk < 2.0f && clk > 0 && anim.GetBool("run"))
                {
                    anim.SetBool("run", false);
                }
                else if (clk <= 0)
                {
                    dir = Mathf.Abs(dir - 1);
                    clk = 5;
                }
            }
        }
        else
        {
            if (anim.GetCurrentAnimatorStateInfo(0).fullPathHash == Animator.StringToHash("Base Layer.ene_hit") || anim.GetCurrentAnimatorStateInfo(0).fullPathHash == Animator.StringToHash("Base Layer.ene_die"))
            {
                clk2 -= Time.deltaTime;
                if (clk2 <= 0) anim.speed = 0;
            }
            
        }
    }
    void Update()
    {
       if(isreset.text == "true" && !reset)
        {
            reset = true;
            die = false;
            eenemy.transform.position = pos;
            anim.SetBool("run", false);
            anim.SetBool("atk", false);
            anim.SetBool("hit", false);
            Color ap = range.GetComponent<SpriteRenderer>().color;
            range.GetComponent<SpriteRenderer>().color = new Color(ap.r, ap.g, ap.b, 0.051f);
            anim.speed = 1;
            dir = orign_dir;
            clk = 5;
            clk3 = 0.56f;
        }
       if (isreset.text == "false" && reset)
       {
            reset = false;
            clk2 = 1.0f;
       }
    }

    void state()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).fullPathHash == Animator.StringToHash("Base Layer.player_atk"))
        {
            anim.SetBool("atk", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "shuli")
        {
            if (!collision.GetComponent<shuli>().onwall)
            {
                if (!die)
                {
                    AS.PlayOneShot(hurt);
                    int count = int.Parse(kill_count.text);
                    count += 1;
                    kill_count.text = count.ToString();
                    Debug.Log("die");
                }
                anim.SetBool("hit", true);
                die = true;
                Color ap = range.GetComponent<SpriteRenderer>().color;
                range.GetComponent<SpriteRenderer>().color = new Color(ap.r,ap.g,ap.b,0);
            }
        }
       
    }
}
