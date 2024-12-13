using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class AntSpawner : MonoBehaviour
{
    public GameObject ant;
    public Vector3 targetPosition;
    public float spawnDelay;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(generateAnt());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator generateAnt()
    {
        yield return new WaitForSeconds(spawnDelay);
        int randomZPosition = Random.Range(0, 100);

        GameObject newAnt = Instantiate(ant, new Vector3(2f, 0f, randomZPosition), Quaternion.Euler(0f, 90f, 0f));
        
        targetPosition = new Vector3(95f, 1f, Random.Range(0f, 100f));
        AIPath aiPath = newAnt.GetComponent<AIPath>();
        aiPath.destination = targetPosition;
        aiPath.SearchPath();
        StartCoroutine(generateAnt());
    }
}
