
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

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayGolemDeath();
        }

        Destroy(gameObject);
    }

    void OnGUI()
    {
        if (currentHealth <= 0 || Camera.main == null)
            return;

        Vector3 screenPosition = Camera.main.WorldToScreenPoint(
            transform.position + Vector3.up * 1.5f
        );

        if (screenPosition.z <= 0)
            return;

        GUIStyle style = new GUIStyle(GUI.skin.box);

        style.fontSize = 24;
        style.fontStyle = FontStyle.Bold;
        style.alignment = TextAnchor.MiddleCenter;

        float x = screenPosition.x - 70;
        float y = Screen.height - screenPosition.y - 22;

        GUI.Box(
            new Rect(x, y, 140, 45),
            $"HP: {currentHealth}/{maxHealth}",
            style
        );
    }
}
