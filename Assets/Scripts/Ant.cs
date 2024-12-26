using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Ant : MonoBehaviour
{
    AIPath aiPath;
    private Game gameManager;
    // Start is called before the first frame update
    void Start()
    {
        aiPath = this.GetComponent<AIPath>();
        GameObject gameManagerObj = GameObject.Find("GameManager");
        gameManager = gameManagerObj.GetComponent<Game>();
    }

    // Update is called once per frame
    void Update()
    {
        if(aiPath.reachedDestination)
        {
            gameManager.lives -= 1;
            Destroy(this.gameObject);
        }
    }

    // void FixedUpdate()
    // {
    //     this.transform.position += Vector3.right * 5f * Time.deltaTime; 
    // }

    void OnPathComplete(Path p)
    {
        
    }
}
