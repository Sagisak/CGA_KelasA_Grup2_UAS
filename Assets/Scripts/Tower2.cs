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

        Debug.Log("Shooting at target: " + target.name);
        GameObject projectileGO = Instantiate(projectilePrefab, FindChildByName(transform, "ProjectilePoint").position, transform.rotation);
        Projectile projectile = projectileGO.GetComponent<Projectile>();

        if (projectile != null)
        {
            projectile.Seek(target);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
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