using UnityEngine; // Necesario para ScriptableObject
using System; // Necesario para Serializable

// Atributo para crear instancias de este ScriptableObject desde el editor de Unity
// fileName: el nombre por defecto del archivo al crearlo
// menuName: la ruta en el men� "Assets/Create/" donde aparecer� la opci�n
[CreateAssetMenu(fileName = "NewCardData", menuName = "Deckfense/Card Data")]
public class CardData : ScriptableObject
{
    // Nombre de la carta (ej: "Lluvia de Agujas")
    public string cardName = "Nueva Carta";

    // Descripci�n del efecto de la carta
    [TextArea(3, 5)] // Hace que la descripci�n sea un �rea de texto multilinea en el Inspector
    public string description = "Descripci�n del efecto de la carta.";

    // Sprite o imagen que representa la carta en la UI
    public Sprite iconSprite;

    // Tiempo de recarga de la carta despu�s de ser jugada, en segundos
    public float cooldownTime = 5f;

    // --- Definici�n de Tipos de Efecto ---
    // Enum para categorizar los diferentes tipos de efectos que una carta puede tener
    public enum EffectType
    {
        None,             // Sin efecto (o efecto no definido)
        AreaDamage,       // Da�o en un �rea espec�fica
        Trap,             // Coloca una trampa temporal
        TerrainModifier,  // Modifica el terreno (ralentiza, bloquea)             
        EffectDuplicator  // Duplica el efecto de la siguiente carta (Eco de guerra)
    }

    // El tipo de efecto que esta carta espec�fica activar�
    public EffectType effectType = EffectType.None;

    // --- Par�metros de Efecto ---
    // Esta clase serializable contendr� todos los par�metros que los diferentes efectos puedan necesitar.
    // Al ser [Serializable], Unity Inspector podr� mostrar y editar sus valores.
    [Serializable]
    public class EffectParameters
    {
        [Header("Par�metros Comunes")]
        public float duration = 0f;    // Duraci�n del efecto (para trampas, ceguera, ayudantes)
        public float radius = 0f;      // Radio de acci�n para efectos de �rea (da�o, ceguera)
        public float damage = 0f;      // Cantidad de da�o (para da�o de �rea, trampas, ayudantes)

        [Header("Par�metros Espec�ficos")]
        public float slowPercentage = 0f; // Porcentaje de ralentizaci�n (para TerrainModifier)
        public int helperAttackCount = 0; // N�mero de ataques para ayudantes (para HelperSpawn)
        // Puedes a�adir m�s par�metros aqu� seg�n la complejidad de tus cartas
    }

    // Instancia de los par�metros espec�ficos para el efecto de esta carta
    public EffectParameters parameters;

    // Constructor vac�o, necesario para ScriptableObjects
    // No se llama autom�ticamente, es m�s una convenci�n.
    public CardData()
    {
        parameters = new EffectParameters();
    }
}