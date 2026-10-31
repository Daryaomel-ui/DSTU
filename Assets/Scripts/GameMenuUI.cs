
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenuUI : MonoBehaviour
{
    public GameObject startPanel;
    public GameObject pausePanel;
    public GameObject deathPanel;
    public GameObject winPanel;

    private enum GameState
    {
        Start,
        Playing,
        Paused,
        Dead,
        Won
    }

    private GameState state = GameState.Start;

    public bool IsPlaying
    {
        get { return state == GameState.Playing; }
    }

    void Awake()
    {
        Time.timeScale = 0f;
        UpdatePanels();
    }

    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            if (state == GameState.Playing)
                PauseGame();
            else if (state == GameState.Paused)
                ResumeGame();
        }
    }

    void UpdatePanels()
    {
        startPanel.SetActive(state == GameState.Start);
        pausePanel.SetActive(state == GameState.Paused);
        deathPanel.SetActive(state == GameState.Dead);
        winPanel.SetActive(state == GameState.Won);
    }

    public void StartGame()
    {
        state = GameState.Playing;
        UpdatePanels();
        Time.timeScale = 1f;
    }

    public void PauseGame()
    {
        if (!IsPlaying) return;

        state = GameState.Paused;
        UpdatePanels();
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        if (state != GameState.Paused) return;

        state = GameState.Playing;
        UpdatePanels();
        Time.timeScale = 1f;
    }

    public void ShowDeath()
    {
        if (!IsPlaying) return;

        state = GameState.Dead;
        UpdatePanels();
        Time.timeScale = 0f;

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayDefeat();
        }
    }

    public void ShowWin()
    {
        if (!IsPlaying) return;

        state = GameState.Won;
        UpdatePanels();
        Time.timeScale = 0f;

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayVictory();
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(
            SceneManager.GetActiveScene().name
        );
    }
}
