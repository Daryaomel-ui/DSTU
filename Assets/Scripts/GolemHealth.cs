
using UnityEngine;

public class GolemHealth : MonoBehaviour
{
    public int maxHealth = 5;

    private int currentHealth;
    private bool isDead = false;
    private GameUI gameUI;

    void Start()
    {
        currentHealth = maxHealth;
        gameUI = FindFirstObjectByType<GameUI>();
    }

    public void TakeDamage(int damage)
    {
        if (isDead)
        {
            return;
        }

        currentHealth -= damage;

        Debug.Log(gameObject.name + " получил урон! Осталось HP: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;

        if (gameUI != null)
        {
            gameUI.AddKill();
        }

        Debug.Log("Голем уничтожен!");
        Destroy(gameObject);
    }
}
