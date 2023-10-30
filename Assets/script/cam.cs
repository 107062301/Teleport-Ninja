using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class cam : MonoBehaviour
{
    // Start is called before the first frame update
    public RectTransform player;
    float limit = 22.03f;

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "game1") limit = 51.4f;
        else limit = 22.03f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);

        if (transform.position.x < 15.62f)
        {
            transform.position = new Vector3(15.62f,transform.position.y,transform.position.z);
        }
        if (transform.position.x > limit)
        {
            transform.position = new Vector3(22.03f, transform.position.y, transform.position.z);
        }

        if (player.position.y > 9.47f)
        {
            transform.position = new Vector3(transform.position.x, player.transform.position.y, transform.position.z);
        }
    }
}
