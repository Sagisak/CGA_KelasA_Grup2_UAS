using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public int lives;
    public int money;

    // Start is called before the first frame update
    void Start()
    {
        lives = 100;
        money = 1000;
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