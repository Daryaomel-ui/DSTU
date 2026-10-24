
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 5;
    public float invulnerabilityTime = 1f;
    public GameUI gameUI;

    private int currentHealth;
    private float nextDamageTime;

    void Start()
    {
        currentHealth = maxHealth;

        if (gameUI != null)
        {
            gameUI.SetHealth(currentHealth);
        }
    }

    public void TakeDamage(int damage)
    {
        if (currentHealth <= 0 ||
            Time.time < nextDamageTime)
        {
            return;
        }

        currentHealth = Mathf.Max(
            0,
            currentHealth - damage
        );

        nextDamageTime =
            Time.time + invulnerabilityTime;

        if (gameUI != null)
        {
            gameUI.SetHealth(currentHealth);
        }

        Debug.Log("HP: " + currentHealth);

        if (currentHealth == 0)
        {
            Debug.Log("Игрок проиграл!");
        }
    }
}
