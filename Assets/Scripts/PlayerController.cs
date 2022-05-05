
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    float speed, h, v;

    bool flip;
    bool invisible;

    string state;

    [SerializeField] GameObject attackSprite;
    [SerializeField] GameObject killedSprite;

    Animator anim;
    Rigidbody2D rb;
    SpriteRenderer rend;


    // Start is called before the first frame update
    void Start()
    {
        speed = 6.0f;

        invisible = false;

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
        }
    }

    void OnCollisionEnter2D(Collision2D other) 
    {
        //        if(other.gameObject.tag == "Enemy")
        switch(other.gameObject.tag)
        {
            case "Enemy":
                OrcController.SetAlarm(false);
            
                Instantiate(killedSprite, gameObject.transform.position, Quaternion.identity);
        
                Destroy(gameObject);

                break;

            case "Potion":
                Destroy(other.gameObject);

                ToggleInvisibility(true);

                break;

            case "Money":
                Destroy(other.gameObject);

                ToggleInvisibility(false);

                break;
        }
    }

    bool CheckInput()
    {
        h = Input.GetAxis("Horizontal");        
        v = Input.GetAxis("Vertical");

        if (Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(attackSprite);

            ToggleInvisibility(false);
        }

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
        if (h < -0.002f)
        {
            flip = true;

            rend.flipX = flip;
        }
        else if (h > 0.002f)
        {
            flip = false;

            rend.flipX = flip;
        }
    }

    public bool GetFlip()
    {
        return flip;
    }

    void ToggleInvisibility(bool inv)
    {
        invisible = inv;

        Color tmp = rend.color;

        if (invisible)
            tmp.a = 0.25f;
        else
            tmp.a = 1.0f;

        rend.color = tmp;
    }
}

