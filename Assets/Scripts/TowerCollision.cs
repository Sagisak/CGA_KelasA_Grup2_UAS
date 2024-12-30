using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerCollision : MonoBehaviour
{
    public bool isColliding = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Tower"))
        {
            isColliding = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Tower"))
        {
            isColliding = false;
        }
    }
}

