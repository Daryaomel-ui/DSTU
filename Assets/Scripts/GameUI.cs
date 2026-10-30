
using UnityEngine;
using TMPro;

public class GameUI : MonoBehaviour
{
    public TMP_Text healthText;
    public TMP_Text timerText;
    public TMP_Text killsText;
    public TMP_Text fireText;
    public TMP_Text iceText;

    private float timeLeft = 120f;
    private int health = 5;
    private int kills = 0;
    private bool fireSelected = true;

    void Start()
    {
        UpdateUI();
    }

    void Update()
    {
        timeLeft = Mathf.Max(
            0f,
            timeLeft - Time.deltaTime
        );

        if (Input.GetButtonDown("SelectFire"))
            fireSelected = true;

        if (Input.GetButtonDown("SelectIce"))
            fireSelected = false;

        UpdateUI();
    }

    void UpdateUI()
    {
        healthText.text = $"HP: {health}/5";
        killsText.text = $"GOLEMS: {kills}/10";

        int seconds = Mathf.CeilToInt(timeLeft);

        timerText.text =
            $"{seconds / 60:00}:{seconds % 60:00}";

        fireText.color = fireSelected
            ? Color.yellow : Color.gray;

        iceText.color = fireSelected
            ? Color.gray : Color.cyan;
    }

    public void SetHealth(int value)
    {
        health = Mathf.Clamp(value, 0, 5);
        UpdateUI();
    }

    public void AddKill()
    {
        kills++;
        UpdateUI();
    }
}

