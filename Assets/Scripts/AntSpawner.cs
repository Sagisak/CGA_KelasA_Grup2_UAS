using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntSpawner : MonoBehaviour
{
    public GameObject ant;
    private float spawnDelay;

    // Start is called before the first frame update
    void Start()
    {
        spawnDelay = 3f;
        StartCoroutine(generateAnt());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator generateAnt()
    {
        yield return new WaitForSeconds(spawnDelay);
        int randomZPosition = Random.Range(20, 80);

        GameObject newAnt = Instantiate(ant, new Vector3(-30f, 0f, randomZPosition), Quaternion.Euler(0f, 90f, 0f));
        StartCoroutine(generateAnt());
    }
}
