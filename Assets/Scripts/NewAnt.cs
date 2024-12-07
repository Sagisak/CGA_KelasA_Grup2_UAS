using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class NewAnt : MonoBehaviour
{
    public float speed = 5f;
    public float waypointDistance = 1f; // Distance to the next waypoint before advancing

    private Seeker seeker;
    private Path path;
    private int currentWaypoint = 0;

    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();

        // Generate a random target on the walkable grid
        GridGraph gg = AstarPath.active.data.gridGraph;
        GraphNode targetNode;
        do
        {
            int randomX = Random.Range(0, gg.width);
            int randomZ = Random.Range(0, gg.depth);
            targetNode = gg.GetNode(randomX, randomZ);
        }
        while (!targetNode.Walkable);

        Vector3 targetPosition = (Vector3)targetNode.position;
        Debug.Log($"Ant target position: {targetPosition}");

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
            Destroy(this.gameObject); // Destroy ant when it reaches the destination
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

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0; // Reset waypoint index
        }
        else
        {
            Debug.LogError($"Pathfinding error: {p.errorLog}");
        }
    }
}