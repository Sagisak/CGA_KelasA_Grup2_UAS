using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    public float speed = 0.1f; // Speed factor to control movement speed

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float verticalInput = Input.GetAxis("Vertical");
        
        if (verticalInput != 0)
        {
            if (transform.position.z < 78f && transform.position.z > 17f)
            {
                // Apply speed factor to verticalInput for slower movement
                transform.position += new Vector3(0, 0, verticalInput * speed);
            }else if(transform.position.z >= 75f && verticalInput < 0){
                transform.position += new Vector3(0, 0, verticalInput * speed);
            }else if(transform.position.z <= 35f && verticalInput > 0){
                transform.position += new Vector3(0, 0, verticalInput * speed);
            }
        }
    }
}
