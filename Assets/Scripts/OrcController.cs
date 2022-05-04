using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcController : MonoBehaviour
{
    static bool alarm;
    string state;

    Animator anim;
    SpriteRenderer rend;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rend = GetComponentInChildren<SpriteRenderer>();

        state = "idle";        
    }

    // Update is called once per frame
    void Update()
    {
        // if(Input.anyKeyDown)
        //     SetAlarm(true);

        switch(state)
        {
            case "idle":
                if(alarm)
                {
                    anim.Play("OrcWalk");

                    state = "walk";
                }

                break;

            case "walk":

                break;
        }

    
    }

    void Flip()
    {
        if(Mathf.Sign(transform.right.x) < 0)
            rend.flipX = true;
        else if(Mathf.Sign(transform.right.x) > 0)
            rend.flipX = false;
    }

    public void SetAlarm(bool a)
    {
        alarm = a;
    }
}
