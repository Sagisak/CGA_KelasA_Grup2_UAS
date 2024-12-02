using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ant : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(this.transform.position.x > 100f)
        {
            Destroy(this.gameObject);
        }    
    }

    void FixedUpdate()
    {
        this.transform.position += Vector3.right * 5f * Time.deltaTime; 
    }
}
