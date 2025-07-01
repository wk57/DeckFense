using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public WaveManager waveManager;

    public TextMeshProUGUI healthText;
    public TextMeshProUGUI scoreTextUI;
    public TextMeshProUGUI waveText;    

    public GameObject gameOverPanel;
    public TMPro.TextMeshProUGUI scoreText;

    public int playerHealth = 5;
    public int playerScore = 0;
    public int currentWave = 1;

    private void Start()
    {
        StartNextWave();
    }
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void StartNextWave()
    {
        waveManager.StartWave(currentWave * 3); // Ejemplo: oleada 1 = 3 enemigos, oleada 2 = 6, etc.
        UpdateUI();
    }

    public void PlayerTakeDamage(int amount)
    {
        playerHealth -= amount;
        if (playerHealth <= 0)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        Debug.Log("Game Over");
        Time.timeScale = 0f; // Pausa el juego
        gameOverPanel.SetActive(true);
        scoreText.text = "Puntuación: " + playerScore.ToString();
    }

    public void Retry()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    public void UpdateUI()
    {
        if (healthText != null)
            healthText.text = "Health: " + playerHealth;

        if (scoreTextUI != null)
            scoreTextUI.text = "Score: " + playerScore;

        if (waveText != null)
            waveText.text = "Wave: " + currentWave;
    }

    
}
