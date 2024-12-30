using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class TowerPlacement : MonoBehaviour
{
    public GameObject[] towerPrefabs; // Array of tower prefabs
    private GameObject currentPreview;
    private int selectedTowerIndex;
    private GameObject selectedTowerPrefab;
    private bool isPlacing = false;
    private Game game;

    private GridGraph gridGraph; // Reference to the GridGraph
    private const float lockedYPosition = 0f; // Lock towers at Y position

    void Start()
    {
        game = FindObjectOfType<Game>();
        // Load the projectile prefab from the Resources folder

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
        selectedTowerIndex = towerIndex;
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
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.red);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            // Get grid position from world position
            Vector3 worldPosition = hit.point;
            var nearestNode = gridGraph.GetNearest(worldPosition).node;
            GridNode gridNode = nearestNode as GridNode;

            Vector3 snappedPosition = new Vector3(
                Mathf.Floor(worldPosition.x), lockedYPosition, Mathf.Floor(worldPosition.z)
            );
            currentPreview.transform.position = snappedPosition;


            if (gridNode != null && gridNode.Walkable)
            {
                // If the node is walkable, show placement is invalid
                currentPreview.GetComponentInChildren<Renderer>().material.color = new Color(1, 0, 0, 0.7f); // Red
            }
            else
            {
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
            if(selectedTowerIndex == 0 && game.money > 200)
            {
                GameObject towerGO = Instantiate(selectedTowerPrefab, currentPreview.transform.position, Quaternion.identity);
                Tower2 towerComponent = towerGO.AddComponent<Tower2>();
                towerComponent.projectilePrefab = Resources.Load<GameObject>("Projectile1");
                towerComponent.GetComponent<Tower2>().range = 10f;
                towerComponent.GetComponent<Tower2>().fireRate = 1f;
                game.DecreaseMoney(200);
            }
            else if(selectedTowerIndex == 1 && game.money > 300)
            {
                GameObject towerGO = Instantiate(selectedTowerPrefab, currentPreview.transform.position, Quaternion.identity);
                Tower2 towerComponent = towerGO.AddComponent<Tower2>();
                towerComponent.projectilePrefab = Resources.Load<GameObject>("Projectile2");
                towerComponent.GetComponent<Tower2>().range = 20f;
                towerComponent.GetComponent<Tower2>().fireRate = 1f;
                game.DecreaseMoney(300);
            }
            else if (selectedTowerIndex == 2 && game.money > 500)
            {
                GameObject towerGO = Instantiate(selectedTowerPrefab, currentPreview.transform.position, Quaternion.identity);
                Tower2 towerComponent = towerGO.AddComponent<Tower2>();
                towerComponent.projectilePrefab = Resources.Load<GameObject>("Projectile3");
                towerComponent.GetComponent<Tower2>().range = 15f;
                towerComponent.GetComponent<Tower2>().fireRate = 3f;
                game.DecreaseMoney(500);
            }

            CancelPlacement(); // End the placement process
        }
    }

    private void CancelPlacement()
    {
        if (currentPreview != null)
        {
            Destroy(currentPreview);
            currentPreview = null;
        }
        isPlacing = false;
    }

    private void SetPreviewMaterialTransparent(GameObject previewObject)
    {
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
                material.renderQueue = 3000;
            }
        }
    }

    private void AddRendererComponent(GameObject previewObject)
    {
        if (previewObject.GetComponentInChildren<Renderer>() == null)
        {
            previewObject.AddComponent<MeshRenderer>();
        }
    }
}