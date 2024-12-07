using Pathfinding;
using UnityEngine;

public class NodeConnection : MonoBehaviour
{
    public int gridSize = 100;
    public float nodeSize = 1f;

    void Start()
    {
        // Access the GridGraph
        GridGraph gg = AstarData.active.graphs[0] as GridGraph;
        gg.SetDimensions(gridSize, gridSize, nodeSize);

        // Scan the graph to initialize it
        AstarData.active.Scan(gg);

        // Add connections between nodes
        ConnectNodes(gg);
    }

    void ConnectNodes(GridGraph gg)
    {
        // Iterate through all nodes in the graph
        for (int y = 0; y < gridSize; y++)
        {
            for (int x = 0; x < gridSize; x++)
            {
                GraphNode currentNode = gg.GetNode(x, y);

                // Skip non-walkable nodes
                if (!currentNode.Walkable) continue;

                // Connect to neighbors (4-way or 8-way)
                ConnectToNeighbor(gg, currentNode, x, y, x + 1, y);     // Right
                ConnectToNeighbor(gg, currentNode, x, y, x - 1, y);     // Left
                ConnectToNeighbor(gg, currentNode, x, y, x, y + 1);     // Up
                ConnectToNeighbor(gg, currentNode, x, y, x, y - 1);     // Down

                // Optional: Add diagonal connections for 8-way
                ConnectToNeighbor(gg, currentNode, x, y, x + 1, y + 1); // Up-Right
                ConnectToNeighbor(gg, currentNode, x, y, x - 1, y + 1); // Up-Left
                ConnectToNeighbor(gg, currentNode, x, y, x + 1, y - 1); // Down-Right
                ConnectToNeighbor(gg, currentNode, x, y, x - 1, y - 1); // Down-Left
            }
        }
    }

    void ConnectToNeighbor(GridGraph gg, GraphNode currentNode, int x1, int y1, int x2, int y2)
    {
        // Check if neighbor is within bounds
        if (x2 < 0 || y2 < 0 || x2 >= gridSize || y2 >= gridSize) return;

        // Get the neighbor node
        GraphNode neighborNode = gg.GetNode(x2, y2);

        // Ensure neighbor is walkable
        if (neighborNode == null || !neighborNode.Walkable) return;

        // Calculate the connection cost (Manhattan distance or diagonal)
        uint cost = (x1 != x2 && y1 != y2) ? (uint)(Mathf.Sqrt(2) * 1000) : 1000;

        // Add a bidirectional connection between the nodes
        currentNode.AddConnection(neighborNode, cost);
        neighborNode.AddConnection(currentNode, cost);
    }
}
