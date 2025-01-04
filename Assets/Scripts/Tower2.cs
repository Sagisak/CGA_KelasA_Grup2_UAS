using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower2 : MonoBehaviour
{
    public float range;
    public float fireRate;
    public GameObject projectilePrefab;
    private float fireCountdown = 0f;
    private Transform target;
    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.positionCount = 50; // Number of segments in the circle
        lineRenderer.useWorldSpace = false;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.loop = true;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default")) { color = Color.red };
        DrawRangeCircle();
    }

    void Update()
    {
        UpdateTarget();

        if (target == null)
            return;

        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    void UpdateTarget()
    {
        GameObject[] ants = GameObject.FindGameObjectsWithTag("Ant");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestAnt = null;

        foreach (GameObject ant in ants)
        {
            float distanceToAnt = Vector3.Distance(transform.position, ant.transform.position);
            if (distanceToAnt < shortestDistance)
            {
                shortestDistance = distanceToAnt;
                nearestAnt = ant;
            }
        }

        if (nearestAnt != null && shortestDistance <= range)
        {
            target = nearestAnt.transform;
            FindChildByName(transform, "ProjectilePoint").parent.LookAt(target);
        }
        else
        {
            target = null;
        }
    }

    void Shoot()
    {
        if (projectilePrefab == null)
        {
            Debug.LogError("Projectile prefab is not assigned!");
            return;
        }

        GameObject projectileGO = Instantiate(projectilePrefab, FindChildByName(transform, "ProjectilePoint").position, transform.rotation);
        Projectile projectile = projectileGO.GetComponent<Projectile>();

        if (projectile != null)
        {
            projectile.Seek(target);
        }
    }

    void DrawRangeCircle()
    {
        float deltaTheta = (2f * Mathf.PI) / lineRenderer.positionCount;
        float theta = 0f;

        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            float x = range * Mathf.Cos(theta);
            float z = range * Mathf.Sin(theta);
            Vector3 pos = new Vector3(x, 0, z);
            lineRenderer.SetPosition(i, pos);
            theta += deltaTheta;
        }
    }

    Transform FindChildByName(Transform parent, string name)
    {
        foreach (Transform child in parent)
        {
            if (child.name == name)
                return child;

            // Recursively search in the child's children
            Transform found = FindChildByName(child, name);
            if (found != null)
                return found;
        }
        return null; // Return null if no matching child is found
    }
}