using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEff : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Debug.Log("effect");
        
        Destroy(gameObject, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
