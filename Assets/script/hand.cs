using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hand : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D rb;
    bool md = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (md)
        {
            rb.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 rs =  Vector2.ClampMagnitude(rb.position, 30);
            rb.position = rs;
        }
        //transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }

    void OnMouseDown()
    {
        md = true;
        rb.isKinematic = true;
        Debug.Log("dd");
    }
    void OnMouseUp()
    {
        md = false;
        rb.isKinematic = false;
    }
}
