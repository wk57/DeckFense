using UnityEngine; // Necesario para ScriptableObject

// Atributo para crear instancias de este ScriptableObject desde el editor de Unity
// fileName: el nombre por defecto del archivo al crearlo
// menuName: la ruta en el men� "Assets/Create/" donde aparecer� la opci�n
[CreateAssetMenu(fileName = "NewEnemyData", menuName = "Deckfense/Enemy Data")]
public class EnemyData : ScriptableObject
{
    // Nombre del tipo de enemigo (ej: "Goblin", "Troll")
    public string enemyName = "Nuevo Enemigo";

    // Salud m�xima del enemigo
    public float health = 100f;

    // Velocidad de movimiento del enemigo a lo largo del camino
    public float speed = 1f; // Unidades por segundo

    // Da�o que el enemigo inflige a la base del jugador si llega al final del camino
    public float damageToBase = 10f;

    // Puntuaci�n que otorga este enemigo al ser derrotado
    public int scoreValue = 10;

    // Referencia al Sprite (para 2D) o al Modelo (para 3D) que representa visualmente a este enemigo
    // Puedes usar un Sprite para juegos 2D o un GameObject/Prefab si planeas usar modelos 3D complejos
    public Sprite enemySprite;
    // Si usas 3D, podr�as tener: public GameObject enemyPrefabModel;
}