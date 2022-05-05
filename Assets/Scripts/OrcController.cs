
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class OrcController : MonoBehaviour
{
    [SerializeField] bool facingLeft;

    static bool alarm;

    float t = 0.0f;

    string state;

    GameObject player;

    [SerializeField] GameObject killedSprite;


    Vector3 oldPos, newPos;

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
        
        setter.target = null;

        oldPos = transform.position;
        newPos = transform.position;

//        facingRight = false;
        rend.flipX = facingLeft;

        state = "idle";

    }

    private void FixedUpdate() 
    {
        Watch();
    }

    // Update is called once per frame
    void Update()
    {
//        if(Input.anyKeyDown)
//            SetAlarm(true);

        switch(state)
        {
            case "idle":
                if(alarm)
                {
                    anim.Play("OrcWalk");

                    if(player != null)
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
        if(player != null)
        {
            Vector2 facingDir = player.transform.position - transform.position;

            if (Mathf.Sign(facingDir.x) < 0)
            {
                rend.flipX = true;

                facingLeft = true;
            }
            else if (Mathf.Sign(facingDir.x) > 0)
            {
                rend.flipX = false;

                facingLeft = false;
            }
        }
    }

    public static void SetAlarm(bool a)
    {
        alarm = a;
    }

    void Watch()
    {
        Vector3 dir;

        if (state == "idle")
        {
            float x = 1f;

            x = facingLeft ? -x : x;

            dir = new Vector3(x, 0f, 0f);
        }
        else
        {
            newPos = transform.position;

            dir = newPos - oldPos;

            dir.Normalize();

            oldPos = newPos;
        }

        float ang = Mathf.Lerp(70, -70, t);

        Vector3 ray = Quaternion.AngleAxis(ang, Vector3.forward) * dir;

//        Debug.DrawRay(transform.position + dir * 0.5f, ray * 10, Color.green);

        RaycastHit2D rh = Physics2D.Raycast(transform.position + dir * 0.6f, ray);

//        Debug.Log(rh.transform.name);

        if (rh.transform.name == "Player")
            SetAlarm(true);

        t += 0.5f * Time.deltaTime;

        if (t > 1.0f)
            t = 0.0f;
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.tag == "WaveTag")
        {
            Instantiate(killedSprite, gameObject.transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
    }
}
