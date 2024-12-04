using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Ant : MonoBehaviour
{
    public float speed = 5f;

    private Seeker seeker;
    private Path path;
    private int currentWaypoint = 0; // Current waypoint index
    private float waypointDistance = 1f;

    // Start is called before the first frame update
    void Start()
    {
        float randomZ = Random.Range(0f, 100f);
        Vector3 targetPosition = new Vector3(100f, transform.position.y, randomZ);
        Debug.Log(targetPosition);
        seeker = GetComponent<Seeker>();
        seeker.StartPath(transform.position, targetPosition, OnPathComplete);
    }

    // Update is called once per frame
    void Update()
    {
        if (path == null) return;

        // Check if we've reached the end of the path
        if (currentWaypoint >= path.vectorPath.Count)
        {
            Debug.Log("End of path reached.");
            Destroy(this.gameObject);
            return;
        }

        // Move towards the next waypoint
        Vector3 direction = (path.vectorPath[currentWaypoint] - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

        // Check if we've reached the current waypoint
        if (Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]) < waypointDistance)
        {
            currentWaypoint++;
        }
    }

    // void FixedUpdate()
    // {
    //     this.transform.position += Vector3.right * 5f * Time.deltaTime; 
    // }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0; // Reset waypoint index
        }
        else
        {
            Debug.LogError("Pathfinding error: " + p.errorLog);
        }
    }
}
