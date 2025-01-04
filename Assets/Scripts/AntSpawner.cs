using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class AntSpawner : MonoBehaviour
{
    public GameObject ant;
    public Vector3 targetPosition;
    public float spawnDelay;
    private Game game;

    // Start is called before the first frame update
    void Start()
    {
        game = FindObjectOfType<Game>();
        StartCoroutine(generateAnt());
    }

    private IEnumerator generateAnt()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnDelay * game.spawnRateModifier);
            int randomZPosition = Random.Range(0, 100);

            GameObject newAnt = Instantiate(ant, new Vector3(2f, 0f, randomZPosition), Quaternion.Euler(0f, 90f, 0f));
            newAnt.tag = "Ant";
            
            targetPosition = new Vector3(95f, 1f, Random.Range(0f, 100f));
            AIPath aiPath = newAnt.GetComponent<AIPath>();
            aiPath.destination = targetPosition;
            aiPath.SearchPath();

            // Set the health of the new ant based on the current round
            Health antHealth = newAnt.GetComponent<Health>();
            if (antHealth != null)
            {
                float additionalHealth = (game.round - 1) * 20f; // Increase health by 20 for each round
                float initialHealth = 100f + additionalHealth; // Assuming base health is 100
                antHealth.SetInitialHealth(initialHealth);
            }
        }
    }
}