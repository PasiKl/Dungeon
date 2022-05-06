
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;


public class MoveBat : MonoBehaviour
{
    float time, resetTime;

    Vector3 pos;

    GameObject dest;
    GameObject player;

    [SerializeField] GameObject effect;

    AIDestinationSetter setter;

    PlayerController playerScript;


    // Start is called before the first frame update
    void Start()
    {
        time = resetTime = Random.Range(0.3f, 0.7f);

        setter = GetComponent<AIDestinationSetter>();

        dest = new GameObject();

        // dest.transform.position = new Vector3(transform.position.x + 1.0f, transform.position.y + 1.0f, 0.0f);
        pos = new Vector3(transform.position.x + 1.0f, transform.position.y + 1.0f, 0.0f);
        dest.transform.position = pos;

        setter.target = dest.transform;

        player = GameObject.Find("Player");

        playerScript = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;

        if(player != null && Vector3.Distance(transform.position, player.transform.position) < 4.0f)
        {
            if(!playerScript.IsInvisibile())
            {   
                setter.target = player.transform;

                return;
            }
        }
            // else if(time < 0.0f)
        if(time < 0.0f)
        {
            float x = Random.value;
            x = Random.value < 0.5f ? x : -x;
            
            float y = Random.value;
            y = Random.value < 0.5f ? y : -y;
                        
            // dest.transform.position = new Vector3(transform.position.x + x, transform.position.y + y, 0.0f);
            pos.Set(transform.position.x + x, transform.position.y + y, 0.0f);
            dest.transform.position = pos;
            
            setter.target = dest.transform;
        
            time = resetTime;
        }
    }

    void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.tag == "WaveTag")
        {
            Instantiate(effect, gameObject.transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
    }
}
