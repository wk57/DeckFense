using UnityEngine; // Necesario para ScriptableObject
using System; // Necesario para Serializable

// Atributo para crear instancias de este ScriptableObject desde el editor de Unity
// fileName: el nombre por defecto del archivo al crearlo
// menuName: la ruta en el menú "Assets/Create/" donde aparecerá la opción
[CreateAssetMenu(fileName = "NewCardData", menuName = "Deckfense/Card Data")]
public class CardData : ScriptableObject
{
    // Nombre de la carta (ej: "Lluvia de Agujas")
    public string cardName = "Nueva Carta";

    // Descripción del efecto de la carta
    [TextArea(3, 5)] // Hace que la descripción sea un área de texto multilinea en el Inspector
    public string description = "Descripción del efecto de la carta.";

    // Sprite o imagen que representa la carta en la UI
    public Sprite iconSprite;

    // Tiempo de recarga de la carta después de ser jugada, en segundos
    public float cooldownTime = 5f;

    // --- Definición de Tipos de Efecto ---
    // Enum para categorizar los diferentes tipos de efectos que una carta puede tener
    public enum EffectType
    {
        None,             // Sin efecto (o efecto no definido)
        AreaDamage,       // Daño en un área específica
        Trap,             // Coloca una trampa temporal
        TerrainModifier,  // Modifica el terreno (ralentiza, bloquea)             
        EffectDuplicator  // Duplica el efecto de la siguiente carta (Eco de guerra)
    }

    // El tipo de efecto que esta carta específica activará
    public EffectType effectType = EffectType.None;

    // --- Parámetros de Efecto ---
    // Esta clase serializable contendrá todos los parámetros que los diferentes efectos puedan necesitar.
    // Al ser [Serializable], Unity Inspector podrá mostrar y editar sus valores.
    [Serializable]
    public class EffectParameters
    {
        [Header("Parámetros Comunes")]
        public float duration = 0f;    // Duración del efecto (para trampas, ceguera, ayudantes)
        public float radius = 0f;      // Radio de acción para efectos de área (daño, ceguera)
        public float damage = 0f;      // Cantidad de daño (para daño de área, trampas, ayudantes)

        [Header("Parámetros Específicos")]
        public float slowPercentage = 0f; // Porcentaje de ralentización (para TerrainModifier)
        public int helperAttackCount = 0; // Número de ataques para ayudantes (para HelperSpawn)
        // Puedes añadir más parámetros aquí según la complejidad de tus cartas
    }

    // Instancia de los parámetros específicos para el efecto de esta carta
    public EffectParameters parameters;

    // Constructor vacío, necesario para ScriptableObjects
    // No se llama automáticamente, es más una convención.
    public CardData()
    {
        parameters = new EffectParameters();
    }
}