
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class OrcController : MonoBehaviour
{
    static bool alarm;
    string state;

    GameObject player;

    Transform target;

    AIDestinationSetter setter;
    Animator anim;
    SpriteRenderer rend;
    Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");

        anim = GetComponentInChildren<Animator>();
        rend = GetComponentInChildren<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        setter = GetComponent<AIDestinationSetter>();
        
        // target
        setter.target = null;        
         
        state = "idle";

    }

    private void FixedUpdate() 
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown)
            SetAlarm(true);

        switch(state)
        {
            case "idle":
                if(alarm)
                {
                    anim.Play("OrcWalk");

                    setter.target = player.transform;

                    state = "walk";
                }

                break;

            case "walk":
                if(alarm)
                    Flip();
                else
                {
                    anim.Play("OrcIdle");

                    state = "idle";
                }

                break;
        }

    
    }

    void Flip()
    {
        Vector2 facing = player.transform.position - transform.position;

        if(Mathf.Sign(facing.x) < 0)
            rend.flipX = true;
        else if(Mathf.Sign(facing.x) > 0)
            rend.flipX = false;
    }

    public static void SetAlarm(bool a)
    {
        alarm = a;
    }
}
