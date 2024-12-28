using UnityEngine;
using Pathfinding;

public class TowerPlacement : MonoBehaviour
{
    public GameObject[] towerPrefabs; // Array of tower prefabs
    private GameObject currentPreview;
    private GameObject selectedTowerPrefab;
    private bool isPlacing = false;

    private GridGraph gridGraph; // Reference to the GridGraph
    private const float lockedYPosition = 0f; // Lock towers at Y position

    void Start()
    {
        // Get the GridGraph instance
        gridGraph = AstarPath.active.graphs[0] as GridGraph;
        if (gridGraph == null)
        {
            Debug.LogError("GridGraph is not properly configured in AstarPath");
        }
    }

    void Update()
    {
        if (isPlacing)
        {
            HandleTowerPreview();
            HandleMouseInput();
        }
    }

    public void SelectTower(int towerIndex)
    {
        if (towerIndex < 0 || towerIndex >= towerPrefabs.Length) return;

        selectedTowerPrefab = towerPrefabs[towerIndex];
        StartPlacement();
    }

    private void StartPlacement()
    {
        if (selectedTowerPrefab == null) return;

        isPlacing = true;

        // Create a transparent preview of the tower
        currentPreview = Instantiate(selectedTowerPrefab);
        AddRendererComponent(currentPreview);
        SetPreviewMaterialTransparent(currentPreview);
    }

    private void HandleTowerPreview()
    {
        if (currentPreview == null || gridGraph == null) return;

        // Raycast to detect the grid
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            // Get grid position from world position
            Vector3 worldPosition = hit.point;
            var nearestNode = gridGraph.GetNearest(worldPosition).node;
            GridNode gridNode = nearestNode as GridNode;

            if (gridNode != null && gridNode.Walkable)
            {
                // If the node is walkable, show placement is invalid
                currentPreview.GetComponentInChildren<Renderer>().material.color = new Color(1, 0, 0, 0.7f); // Red
            }
            else
            {
                // Snap to grid coordinates (XZ only) and lock Y
                Vector3 snappedPosition = new Vector3(
                    Mathf.Floor(worldPosition.x) + 0.5f, // Center of the grid cell
                    lockedYPosition,                     // Lock Y
                    Mathf.Floor(worldPosition.z) + 0.5f
                );
                currentPreview.transform.position = snappedPosition;

                // Show placement is valid
                currentPreview.GetComponentInChildren<Renderer>().material.color = new Color(0, 1, 0, 0.7f); // Green
            }
        }
    }

    private void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0)) // Left-click
        {
            PlaceTower();
        }
        else if (Input.GetMouseButtonDown(1)) // Right-click
        {
            CancelPlacement();
        }
    }

    private void PlaceTower()
    {
        if (currentPreview == null || gridGraph == null) return;

        // Get grid position from world position
        Vector3 position = currentPreview.transform.position;
        var nearestNode = gridGraph.GetNearest(position).node;
        GridNode gridNode = nearestNode as GridNode;

        if (gridNode != null && !gridNode.Walkable)
        {
            // Place the tower at the preview position
            Instantiate(selectedTowerPrefab, currentPreview.transform.position, Quaternion.identity);

            // Mark the node as non-walkable after placing the tower
            gridNode.Walkable = false;

            CancelPlacement(); // End the placement process
        }
    }

    private void CancelPlacement()
    {
        isPlacing = false;

        if (currentPreview != null)
        {
            Destroy(currentPreview);
        }

        selectedTowerPrefab = null;
    }

    private void SetPreviewMaterialTransparent(GameObject previewObject)
    {
        // Change the material of the preview object to appear semi-transparent
        Renderer[] renderers = previewObject.GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            foreach (Material material in renderer.materials)
            {
                material.SetFloat("_Mode", 2); // Fade mode
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                material.SetInt("_ZWrite", 0);
                material.DisableKeyword("_ALPHATEST_ON");
                material.EnableKeyword("_ALPHABLEND_ON");
                material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                material.color = new Color(material.color.r, material.color.g, material.color.b, 0.7f); // 70% opacity
            }
        }
    }

    private void AddRendererComponent(GameObject previewObject)
    {
        // Ensure the object has a Renderer component
        if (previewObject.GetComponentInChildren<Renderer>() == null)
        {
            previewObject.AddComponent<MeshRenderer>();
        }
    }
}
