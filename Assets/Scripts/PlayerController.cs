
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    float speed, h, v;

    bool flip;

    string state;

    [SerializeField] GameObject attackSprite;

    Animator anim;
    Rigidbody2D rb;
    SpriteRenderer rend;


    // Start is called before the first frame update
    void Start()
    {
        speed = 6.0f;

        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();

        anim.Play("PlayerIdle");

        state = "idle";
    }

    void FixedUpdate() 
    {
        float x, y;

        x = Math.Sign(h) * speed * Time.deltaTime;
        y = Math.Sign(v) * speed * Time.deltaTime;

        rb.MovePosition(transform.position + new Vector3(x, y, 0));
    }

    // Update is called once per frame
    void Update()
    {
        bool move;
        // float x, y;

        move = CheckInput();

        switch(state)
        {
            case "idle":
                if(move)
                {
                    anim.Play("PlayerWalk");
                
                    state = "walk";
                }

                break;

            case "walk":
                if(!move)
                {
                    anim.Play("PlayerIdle");
                
                    state = "idle";
                }

                break;

            // case "attack":

            //     break;
        }
    }

    void OnCollisionEnter2D(Collision2D other) 
    {
        // Debug.Log("hit");
    }

    bool CheckInput()
    {
        h = Input.GetAxis("Horizontal");        
        v = Input.GetAxis("Vertical");

        // if(Input.GetKeyDown(KeyCode.Space))
        if(Input.GetButtonDown("Fire1"))
            Instantiate(attackSprite);
            // Debug.Log("attack");
            // state = "attack";

        if(h != 0)
        {
            Flip(h);

            return true;
        }
        
        if(v != 0)
            return true;

        return false;
    }

    void Flip(float h)
    {
        if(h < 0.002f)
            flip = true;
        else if(h > 0.002f)
            flip = false;

        rend.flipX = flip;
    }

    public bool GetFlip()
    {
        return flip;
    }
}

