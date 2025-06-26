using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public WaveManager waveManager;

    public int playerHealth = 5;

    private void Start()
    {
        waveManager.StartWave();
    }
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void PlayerTakeDamage(int amount)
    {
        playerHealth -= amount;
        Debug.Log("Salud del jugador: " + playerHealth);

        if (playerHealth <= 0)
        {
            Debug.Log("GAME OVER");
            // Aquí puedes activar una pantalla de Game Over
        }
    }
}
