
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class OrcController : MonoBehaviour
{
    [SerializeField] bool facingLeft;
    
    bool seePlayer;

    static bool alarm;

    float t = 0.0f;
    float time, resetTime;

    string state;

    GameObject dest;
    GameObject player;

    [SerializeField] GameObject killedSprite;

    Vector3 dir, oldPos, newPos;

    AIDestinationSetter setter;
    Animator anim;
    SpriteRenderer rend;
    Rigidbody2D rb;

    PlayerController playerScript;


    // Start is called before the first frame update
    void Start()
    {
        time = resetTime = Random.Range(1.0f, 3.0f);

        dest = new GameObject();

        player = GameObject.Find("Player");

        anim = GetComponentInChildren<Animator>();
        rend = GetComponentInChildren<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        setter = GetComponent<AIDestinationSetter>();

        playerScript = player.GetComponent<PlayerController>();

        setter.target = null;

        oldPos = transform.position;
        newPos = transform.position;

//        facingRight = false;
        rend.flipX = facingLeft;

        state = "idle";

        seePlayer = false;
    }

    private void FixedUpdate() 
    {
        Watch();
    }

    // Update is called once per frame
    void Update()
    {
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

            case "search":
                if(player != null)
                    if(seePlayer)
                        setter.target = player.transform;
                    else
                        // setter.target = null;
                        Search();

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

    void Search()
    {
        time -= Time.deltaTime;

        if(time < 0.0f)
        {
            float x = Random.value;
            x = Random.value < 0.5f ? x : -x;
            
            float y = Random.value;
            y = Random.value < 0.5f ? y : -y;

            var pos = new Vector3(transform.position.x + x * 2.0f, transform.position.y + y * 2.0f, 0.0f);

            dest.transform.position = pos;

            Vector2 facingDir = pos - setter.transform.position;

            // if(dir.magnitude == 0.0f)
                setter.target = dest.transform;

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

            time = resetTime;
        }
    }

    public static void SetAlarm(bool a)
    {
        alarm = a;
    }

    void Watch()
    {
        float x = 1f;

        // Vector3 dir;

        if (state == "idle")
        {
            x = facingLeft ? -x : x;

            dir = new Vector3(x, 0f, 0f);
        }
        else
        {
            newPos = transform.position;

            dir = newPos - oldPos;

            dir.Normalize();

            if(dir.magnitude == 0f)
            {
                x = facingLeft ? -x : x;

                dir = new Vector3(x, 0f, 0f);
            }

            oldPos = newPos;
        }

        float ang = Mathf.Lerp(70, -70, t);

        Vector3 ray = Quaternion.AngleAxis(ang, Vector3.forward) * dir;

        Debug.DrawRay(transform.position + dir * 0.5f, ray * 10, Color.green);

        RaycastHit2D rh = Physics2D.Raycast(transform.position + dir * 0.6f, ray);

//        Debug.Log(rh.transform.name);

        if (rh.transform.name == "Player")
        {
            // Debug.Log("player found");

            if(!playerScript.IsInvisibile())
            {
                // Debug.Log("see player");

                seePlayer = true;

                SetAlarm(true);
            }
            else
            {
                // Debug.Log("not seen");
                
                seePlayer = false;
            }
        }

        if(!seePlayer && alarm)
        // if(playerScript.IsInvisibile() && alarm)
        {
            state = "search";
        }

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
