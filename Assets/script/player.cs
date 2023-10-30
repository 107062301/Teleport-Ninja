using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class player : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject wanted;
    public RectTransform pplayer;
    public RectTransform hand, Big, pivot, arrow, aim;
    public Rigidbody2D shuli;
    public AudioClip Slash, mark, shoot, hurt, pick, open, Break, document, comp;
    public Button btn;
    internal Vector3 origin_pos;
    Animator anim;
    AudioSource AS;
    float speed = 4f, ofset = 0, mx, my, vector, magnitude, Dx, Dy, rst_clk = 0.1f;
    public int skey = 0, skey2 = 0;
    bool jp = true, die = false, play_one = false, win = false;
    public bool slash = false, has_key = false, doc = false, has_key2 = false;
    public Text isreset, throw_count, kill_count, gold, wanted_throw_count, wanted_kill_count;
    public float px = 0, clk = 3f, py = 0, clk2 = 3f;
    void Start()
    {
        anim = GetComponent<Animator>();
        AS = GetComponent<AudioSource>();
        px = transform.position.x;
        py = transform.position.y;
        origin_pos = pplayer.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!die && !win)
        {
            jump();
            if (slash)
            {
                if (!play_one)
                {
                    play_one = true;
                    AS.PlayOneShot(mark);
                }
                clk -= Time.deltaTime;
                if (clk <= 0)
                {
                    slash = false;
                    play_one = false;
                    clk = 3f;
                }
                if (Input.GetKeyDown(KeyCode.R))
                {
                    //AS.Stop();
                    play_one = false;
                    AS.PlayOneShot(Slash);
                    pplayer.transform.position = new Vector3(px, py + 2, transform.position.z);
                    pplayer.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 8);
                    GetComponent<Rigidbody2D>().velocity = new Vector2(0, 8);
                    slash = false;
                    clk = 3;
                }
            }
        }
        else
        {
            if (die)
            {
                clk2 -= Time.deltaTime;
                if (clk2 <= 0)
                {
                    SceneManager.LoadScene("lose");
                }
            }
        }

        if (transform.position.y <= -10) die = true;

    }
    void OnMouseDown()
    {


    }
    private void FixedUpdate()
    {

        move();
        if (isreset.text == "true")
        {
            rst_clk -= Time.deltaTime;
            if (rst_clk <= 0)
            {
                kill_count.text = "0";
                throw_count.text = "0";
                skey = 0;
                has_key = false;
                doc = false;
                die = false;
                clk2 = 3f;
                GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
                isreset.text = "false";
                rst_clk = 0.2f;
            }
        }


    }

    void move()
    {
        anim.SetFloat("speed", Mathf.Abs(Input.GetAxis("Horizontal")));
        if (!die && !win)
        {
            pplayer.transform.position += new Vector3(1, 0, 0) * Input.GetAxis("Horizontal") * speed * Time.deltaTime;
            if (Input.GetAxis("Horizontal") < 0)
            {
                ofset = 0.3f;
                pplayer.localScale = new Vector3(-1, 1, 1);

            }
            else if (Input.GetAxis("Horizontal") > 0)
            {
                ofset = -0.3f;
                pplayer.localScale = new Vector3(1, 1, 1);
            }
            mx = transform.position.x + Dx;
            my = transform.position.y + Dy;

            if (Input.GetKey(KeyCode.F))
            {
                anim.SetBool("atk", true);
            }
            if (anim.GetCurrentAnimatorStateInfo(0).fullPathHash == Animator.StringToHash("Base Layer.player_atk"))
            {
                anim.SetBool("atk", false);
            }

            pivot.transform.position = transform.position + new Vector3(ofset, 0, 0);
        }
    }

    void jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jp)
        {
            anim.SetBool("jump", true);
            jp = false;
            GetComponent<Rigidbody2D>().velocity += new Vector2(0, 8);
            pplayer.GetComponent<Rigidbody2D>().velocity += new Vector2(0, 8);
        }
        if (anim.GetCurrentAnimatorStateInfo(0).fullPathHash == Animator.StringToHash("Base Layer.player_atk"))
        {
            anim.SetBool("atk", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "ground")
        {
            if (!(collision.transform.rotation.z == 0 && collision.transform.position.y > transform.position.y))
            {
                anim.SetBool("jump", false);
                jp = true;
            }
        }
        if (collision.gameObject.name == "door" && !collision.gameObject.GetComponent<rst_obj>().destroyed && has_key2)
        {
            has_key2 = false;
            collision.gameObject.GetComponent<rst_obj>().destroyed = true;
            AS.PlayOneShot(open);

        }
        if (collision.gameObject.name == "sdoor" && !collision.gameObject.GetComponent<rst_obj>().destroyed && skey2 == 3)
        {
            skey2 = 0;
            collision.gameObject.GetComponent<rst_obj>().destroyed = true;
            AS.PlayOneShot(open);
        }
        if (collision.gameObject.name == "break")
        {
            AS.PlayOneShot(Break);
        }

    }


    public void MUP()
    {
        if (!die && !win)
        {
            AS.PlayOneShot(shoot);
            arrow.gameObject.SetActive(false);
            aim.gameObject.SetActive(false);
            int count = int.Parse(throw_count.text);
            count += 1;
            throw_count.text = count.ToString();
            Vector3 mv = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            anim.SetBool("atk", true);
            Rigidbody2D s = Instantiate(shuli, transform.position, Quaternion.identity);
            Vector2 v2 = new Vector2(Mathf.Cos(vector), Mathf.Sin(vector));
            s.transform.forward = arrow.transform.forward;
            s.velocity = arrow.transform.up * 5 * magnitude;
            if (mv.x < mx)
            {
                s.velocity = -arrow.transform.up * 5 * magnitude;
            }
            s.angularVelocity = 400f;
        }

        //Debug.Log(arrow.transform.up);

    }
    public void MDown()
    {
        if (!die && !win)
        {
            Vector3 mv = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mx = mv.x;
            my = mv.y;
            Dx = mx - transform.position.x;
            Dy = my - transform.position.y;
            Debug.Log(mx + ":" + my);
        }
    }
    public void MDrag()
    {
        if (!die && !win)
        {
            if (!arrow.gameObject.activeSelf)
            {
                arrow.gameObject.SetActive(true);
                aim.gameObject.SetActive(true);
            }
            Vector3 mv = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float distance = Vector2.Distance(new Vector2(mx, my), new Vector2(mv.x, mv.y));
            float dx, dy, deg;
            dx = mv.x - arrow.position.x;
            dy = mv.y - arrow.position.y;
            deg = Mathf.Atan2(dy, dx);
            arrow.transform.eulerAngles = new Vector3(0, 0, deg * 180 / Mathf.PI - 90);
            vector = deg * 180 / Mathf.PI - 90;
            pplayer.localScale = new Vector3(1, 1, 1);
            magnitude = distance;
            if (magnitude >= 3.3f) magnitude = 3.3f;
            if (mx < mv.x)
            {
                distance *= -1;
                arrow.transform.eulerAngles = new Vector3(0, 0, -deg * 180 / Mathf.PI - 90);
                vector = -deg * 180 / Mathf.PI - 90;
                pplayer.localScale = new Vector3(-1, 1, 1);
                //arrow.localScale = new Vector3(-1, 1, 1);
            }
            float clamp = distance / 10;
            if (clamp >= 0.3f) clamp = 0.3f;
            if (clamp <= -0.3f) clamp = -0.3f;
            arrow.localScale = new Vector2(0.1f, clamp);

        }
        //Debug.Log(distance);
        //Debug.Log(arrow.rotation.z);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);
        if (collision.name == "key" && !collision.GetComponent<rst_obj>().destroyed)
        {
            has_key = true;
            has_key2 = true;
            collision.GetComponent<rst_obj>().destroyed = true;
            AS.PlayOneShot(pick);
            Debug.Log("true");
            //Destroy(collision.gameObject);
        }
        if (collision.name == "sboss" && doc)
        {
            SUCCESS("sboss");
        }
        if (collision.name == "boss")
        {
            SUCCESS("boss");
        }
        if (collision.name == "doc" && !collision.GetComponent<rst_obj>().destroyed)
        {
            collision.GetComponent<rst_obj>().destroyed = true;
            AS.PlayOneShot(document);
            doc = true;
        }
        if (collision.name == "skey" && !collision.GetComponent<rst_obj>().destroyed)
        {
            collision.GetComponent<rst_obj>().destroyed = true;
            AS.PlayOneShot(pick);
            skey += 1;
            skey2 += 1;
        }
        if (collision.tag == "arrow" && !win)
        {
            AS.PlayOneShot(hurt);
            die = true;
            GetComponent<SpriteRenderer>().color = new Color(0, 0, 0);
        }
        if (collision.name == "trap")
        {
            AS.PlayOneShot(hurt);
            die = true;
            GetComponent<SpriteRenderer>().color = new Color(0, 0, 0);
        }
    }

    public void main()
    {
        SceneManager.LoadScene("main");
    }
    public void retry()
    {
        isreset.text = "true";
        pplayer.transform.position = origin_pos;

    }

    void SUCCESS(string name)
    {
        win = true;
        wanted.SetActive(true);
        AS.PlayOneShot(document);
        AS.PlayOneShot(comp);
        wanted_kill_count.text = kill_count.text;
        wanted_throw_count.text = throw_count.text;
        btn.GetComponent<Image>().raycastTarget = true;
        btn.interactable = true;
        float th = 100000 / int.Parse(throw_count.text);
        float ki = int.Parse(kill_count.text) * 1000;
        gold.text = ((int)(th + ki)).ToString() + "$";
    }

    public void continue_game()
    {
        wanted.SetActive(false);
        btn.GetComponent<Image>().raycastTarget = false;
        btn.interactable = false;
        if (SceneManager.GetActiveScene().name == "game1")
        {
            SceneManager.LoadScene("game2");
        }
        else
        {
            SceneManager.LoadScene("WIN");
        }

    }


}
