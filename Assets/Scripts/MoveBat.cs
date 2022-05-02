using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;


public class MoveBat : MonoBehaviour
{
    float time, resetTime;

    GameObject dest;
    GameObject player;

    AIDestinationSetter setter;


    // Start is called before the first frame update
    void Start()
    {
        time = resetTime = Random.Range(0.3f, 0.7f);

        setter = GetComponent<AIDestinationSetter>();

        dest = new GameObject();

        dest.transform.position = new Vector3(transform.position.x + 1.0f, transform.position.y + 1.0f, 0.0f);

        setter.target = dest.transform;

        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;

        if(Vector3.Distance(transform.position, player.transform.position) < 4.0f)
            setter.target = player.transform;
        else if(time < 0.0f)
        {
            float x = Random.value;
            x = Random.value < 0.5f ? x : -x;
            
            float y = Random.value;
            y = Random.value < 0.5f ? y : -y;
                        
            dest.transform.position = new Vector3(transform.position.x + x, transform.position.y + y, 0.0f);

            setter.target = dest.transform;
        
            time = resetTime;
        }
    }
}
