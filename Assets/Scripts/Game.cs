using System.Collections;
using UnityEngine;

public class Game : MonoBehaviour
{
    public int lives;
    public int money;
    public int round = 1;
    public float spawnRateModifier = 1f;
    private float roundTimer = 120f; // 2 minutes in seconds

    // Start is called before the first frame update
    void Start()
    {
        lives = 100;
        money = 1000;
        round = 1;
        Debug.Log("Game started. Initial round: " + round);
        StartCoroutine(RoundTimer());
    }

    private IEnumerator RoundTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(roundTimer);
            IncreaseRound();
        }
    }

    private void IncreaseRound()
    {
        round++;
        spawnRateModifier *= 0.75f; // Increase spawn rate by 1/4
        Debug.Log("Round increased to: " + round + ", Spawn Rate Modifier: " + spawnRateModifier);
    }

    public void DecreaseLives(int amount)
    {
        lives -= amount;
        if (lives <= 0)
        {
            GameOver();
        }
    }

    public void DecreaseMoney(int amount)
    {
        money -= amount;
    }

    public void IncreaseMoney(int amount)
    {
        money += amount;
    }

    private void GameOver()
    {
        Debug.Log("Game Over!");
        // Implement game over logic here (e.g., show game over screen, stop the game, etc.)
    }
}