using UnityEngine;
using TMPro; // Necesario si usas TextMeshPro para la UI

public class GameManager : MonoBehaviour
{
    // Implementaci�n del patr�n Singleton: Una �nica instancia de GameManager
    public static GameManager Instance { get; private set; }

    [Header("Game State")]
    public float baseHealth = 10f; // Salud actual del jugador
    public int playerScore = 0;       // Puntuaci�n actual del jugador
    public int currentWave = 0;       // Oleada actual
    public int enemiesRemainingInWave = 0; // Enemigos restantes en la oleada actual

    // [Opcional] Variables para la gesti�n del tiempo de oleada o entre oleadas
    public float waveTimer = 0f;
    public bool isWaveActive = false;
    public float timeBetweenWaves = 5f; // Tiempo de espera antes de la siguiente oleada

    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI playerHealthText; // Referencia al texto de salud en la UI
    [SerializeField] private TextMeshProUGUI playerScoreText;  // Referencia al texto de puntuaci�n en la UI
    [SerializeField] private TextMeshProUGUI currentWaveText;  // Referencia al texto de oleada en la UI
    [SerializeField] private TextMeshProUGUI enemiesRemainingText; // Referencia al texto de enemigos restantes en la UI

    // M�todo Awake se llama cuando el script se carga, antes que Start
    private void Awake()
    {
        // Asegurarse de que solo haya una instancia de GameManager
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Destruir si ya existe otra instancia
            return;
        }
        Instance = this; // Establecer esta instancia como la �nica
        DontDestroyOnLoad(gameObject); // Para que persista entre escenas si es necesario

        // Inicializaci�n b�sica de variables al inicio del juego
        baseHealth = 100f;
        playerScore = 0;
        currentWave = 0; // Se incrementar� en StartGame() o la primera StartWave()
        enemiesRemainingInWave = 0;
        isWaveActive = false;
    }

    // M�todo Start se llama una vez al inicio del juego
    private void Start()
    {
        // Asegurarse de que la UI se actualice con los valores iniciales
        UpdateUI();
        // Iniciar el juego (podr�a ser llamado desde un bot�n de "Jugar" en un men� principal)
        StartGame();
    }

    // M�todo Update se llama una vez por cada frame
    private void Update()
    {
        // Gesti�n del tiempo de oleada (ejemplo b�sico)
        if (isWaveActive)
        {
            // L�gica de juego activa, los enemigos se mueven, etc.
            // Si no hay enemigos restantes, considerar el fin de la oleada
            if (enemiesRemainingInWave <= 0)
            {
                EndWave();
            }
        }
        else // Si la oleada no est� activa, estamos entre oleadas o al inicio
        {
            waveTimer -= Time.deltaTime;
            if (waveTimer <= 0 && currentWave > 0) // currentWave > 0 para no iniciar la siguiente oleada justo al inicio del juego
            {
                // Si el juego ha iniciado y no es la primera oleada, inicia la siguiente autom�ticamente
                StartWave(currentWave + 1);
            }
        }
    }

    // M�todo para iniciar el juego
    public void StartGame()
    {
        Debug.Log("Game Started!");
        // Aqu� podr�as cargar la primera oleada o mostrar un tutorial
        StartWave(1); // Inicia la primera oleada
    }

    // M�todo para iniciar una nueva oleada
    public void StartWave(int waveNumber)
    {
        currentWave = waveNumber;
        Debug.Log($"Starting Wave {currentWave}!");
        isWaveActive = true;
        waveTimer = 0f; // Resetear el timer de oleada

        // TODO: Llamar al WaveManager para que spawnee los enemigos de esta oleada
        // Por ahora, solo un n�mero de enemigos ficticios
        enemiesRemainingInWave = 5 * currentWave;

        UpdateUI(); // Actualizar la UI para reflejar la nueva oleada
    }

    // M�todo que se llama cuando termina una oleada (todos los enemigos derrotados o pasados)
    public void EndWave()
    {
        Debug.Log($"Wave {currentWave} Ended!");
        isWaveActive = false;
        waveTimer = timeBetweenWaves; // Establecer el tiempo de espera para la siguiente oleada

        // TODO: L�gica de fin de oleada, como dar recompensas, robar nuevas cartas, etc.
        // Si no hay m�s oleadas predefinidas, el jugador gana.
        // If (currentWave >= totalNumberOfWaves) { WinGame(); }
    }

    // M�todo para manejar el da�o al jugador
    public void TakeDamage(float amount)
    {
        baseHealth -= amount;
        if (baseHealth <= 0)
        {
            baseHealth = 0;
            GameOver();
        }
        UpdateUI();
    }

    // M�todo para a�adir puntuaci�n
    public void AddScore(int amount)
    {
        playerScore += amount;
        UpdateUI();
    }

    // M�todo para notificar que un enemigo ha sido eliminado
    public void EnemyDefeated()
    {
        enemiesRemainingInWave--;
        UpdateUI();
        if (enemiesRemainingInWave <= 0 && isWaveActive)
        {
            // Esto lo manejar� el EndWave() llamado desde Update()
            // para asegurar que todos los enemigos hayan sido procesados.
        }
    }

    // M�todo para manejar el fin del juego (Game Over)
    public void GameOver()
    {
        Debug.Log("Game Over!");
        // TODO: Detener todas las mec�nicas del juego, mostrar pantalla de Game Over
        Time.timeScale = 0; // Pausar el tiempo del juego (�til para pantallas de fin)
        // Puedes reiniciar la escena o ir a un men� de Game Over
    }

    // M�todo para actualizar todos los elementos de la UI
    private void UpdateUI()
    {
        if (playerHealthText != null) playerHealthText.text = $"Salud: {baseHealth}";
        if (playerScoreText != null) playerScoreText.text = $"Puntuaci�n: {playerScore}";
        if (currentWaveText != null) currentWaveText.text = $"Oleada: {currentWave}";
        if (enemiesRemainingText != null) enemiesRemainingText.text = $"Enemigos: {enemiesRemainingInWave}";
    }

    // [Opcional] M�todo para reiniciar el juego si est�s en la misma escena
    public void RestartGame()
    {
        Time.timeScale = 1; // Reanudar el tiempo del juego
        // Recargar la escena actual
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}