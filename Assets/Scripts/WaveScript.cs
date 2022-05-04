
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WaveScript : MonoBehaviour
{
    float x;

    GameObject player;

    SpriteRenderer rend;


    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();

        player = GameObject.Find("Player");

        x = 0.6f;

        rend.flipX = player.GetComponent<PlayerController>().GetFlip(); 

        x = rend.flipX ? -x : x;

        transform.position = player.transform.position + new Vector3(x, 0f, 0f);

        Destroy(gameObject, 0.3f);
    }

    // Update is called once per frame
    void Update()
    {
        // float x = 0.6f;

        // rend.flipX = player.GetComponent<PlayerController>().GetFlip(); 

        // x = rend.flipX ? -x : x;

        if(player != null)
            transform.position = player.transform.position + new Vector3(x, 0f, 0f);
    }

    // void OnCollisionEnter2D(Collision2D other) 
    // {
    //     Debug.Log("attack");    
    //     Debug.Log(other.gameObject.name);    
    // }
}
