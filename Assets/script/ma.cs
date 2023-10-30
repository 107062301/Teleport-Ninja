using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ma : MonoBehaviour
{
    // Start is called before the first frame update
    Text mode;
    //public AudioClip a1, a2;
    public GameObject ok;
    public SpriteRenderer ninja;
    public Text title;
    bool down = true, down2 = true;
    static bool destroy = false, a = false, first = true;
    GameObject str, m1, m2, txt, des, DES, cover;
    AudioSource As;
    void Start()
    {
        As = GetComponent<AudioSource>();
        m1 = GameObject.Find("/Canvas/m1");
        m2 = GameObject.Find("/Canvas/m2");
        des = GameObject.Find("/Canvas/des");
        DES = GameObject.Find("/Canvas/DES");
        cover = GameObject.Find("/Canvas/cover");

        m2.GetComponent<Button>().onClick.AddListener(mode2);
        m1.GetComponent<Button>().onClick.AddListener(mode1);
        des.GetComponent<Button>().onClick.AddListener(destination);
        if (first)
        {
            gameObject.name = "fixed";
            first = false;
        }
    }
   
    void Update()
    {
        if (down)
        {
            title.color -= new Color(0,0,0,1) * Time.deltaTime * 0.8f;
            if (title.color.a <= 0.3f) down = false;
        }
        else
        {
            title.color += new Color(0, 0, 0, 1) * Time.deltaTime * 0.8f;
            if (title.color.a >= 1) down = true;
        }

        if (down2)
        {
            ninja.color -= new Color(1, 1, 1, 0) * Time.deltaTime * 0.14f;
            if (ninja.color.r <= 0.4f) down2 = false;
        }
        else
        {
            ninja.color += new Color(1, 1, 1, 0) * Time.deltaTime * 0.14f;
            if (ninja.color.r >= 1) down2 = true;
        }

    }

   
    public void mode1()
    {
        SceneManager.LoadScene("game1");
        
    }
    public void mode2()
    {
        SceneManager.LoadScene("game2");
 
    }
    public void destination()
    {
        DES.GetComponent<Text>().color += new Color(0, 0, 0, 1);
        cover.GetComponent<Image>().color += new Color(0, 0, 0, 0.83f);
        cover.GetComponent<Image>().raycastTarget = true;
        ok.SetActive(true);
    }
    public void Okk()
    {
        DES.GetComponent<Text>().color -= new Color(0, 0, 0, 1);
        cover.GetComponent<Image>().color -= new Color(0, 0, 0, 0.83f);
        cover.GetComponent<Image>().raycastTarget = false;
        ok.SetActive(false);
    }
}
