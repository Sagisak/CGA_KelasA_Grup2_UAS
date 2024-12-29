using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower2 : MonoBehaviour
{
    public float range = 10f;
    public float fireRate = 1f;
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
            Debug.Log("Target acquired: " + target.name);
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
        GameObject projectileGO = Instantiate(projectilePrefab, transform.position, transform.rotation);
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
}