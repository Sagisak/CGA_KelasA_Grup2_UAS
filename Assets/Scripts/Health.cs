using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    public GameObject healthBarPrefab; // Reference to the health bar prefab
    private GameObject healthBarInstance;
    private RectTransform healthBarFillRect; // Use RectTransform instead of Image

    private Game game; // Reference to the Game class

    void Start()
    {
        game = FindObjectOfType<Game>();
        InitializeHealthBar();
        currentHealth = maxHealth;
    }

    private void InitializeHealthBar()
    {
        // Instantiate the health bar and set it as a child of the ant
        healthBarInstance = Instantiate(healthBarPrefab, transform.position + Vector3.up * 2, Quaternion.identity, transform);
        healthBarFillRect = healthBarInstance.transform.GetChild(0).GetChild(0).GetComponent<RectTransform>();

        // Add the Billboard script to the health bar instance
        healthBarInstance.AddComponent<Billboard>();
    }

    public void SetInitialHealth(float initialHealth)
    {
        if (healthBarFillRect == null)
        {
            InitializeHealthBar();
        }

        maxHealth = initialHealth;
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;

        // Update the health bar width
        UpdateHealthBar();

        if (currentHealth <= 0f)
        {
            Remove();
        }
    }

    public void IncreaseHealth(float amount)
    {
        maxHealth += amount;
        currentHealth = maxHealth;

        // Update the health bar width
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        if (healthBarFillRect == null)
        {
            Debug.LogError("healthBarFillRect is null. Cannot update health bar.");
            return;
        }

        float healthPercentage = currentHealth / maxHealth;
        healthBarFillRect.sizeDelta = new Vector2(healthPercentage * healthBarFillRect.rect.width, healthBarFillRect.sizeDelta.y);
    }

    void Remove()
    {
        Destroy(healthBarInstance); // Destroy the health bar
        Destroy(gameObject);

        // Increase the player's money by 5
        if (game != null)
        {
            game.IncreaseMoney(5);
        }
    }
}