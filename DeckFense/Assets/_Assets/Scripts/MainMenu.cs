using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene(1); // Usa el índice de la escena del juego
        // O usa por nombre: SceneManager.LoadScene("NombreDeTuEscena");
    }
}
