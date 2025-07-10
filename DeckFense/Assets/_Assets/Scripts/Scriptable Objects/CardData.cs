using UnityEngine;

public enum CardEffectType { DamageArea, SlowArea, SlowAndDamageArea }

[CreateAssetMenu(fileName = "NewCard", menuName = "Cards/CardData")]
public class CardData : ScriptableObject
{
    public string cardName;
    public Sprite icon;
    public CardEffectType effectType;
    public float effectValue;      // daño o porcentaje de ralentización
    public float effectRadius;
    //public float cooldown;
    public float cooldownTime = 3f;

    public GameObject visualEffectPrefab;

    public float slowFactor = 0.5f;     // Solo aplica si hay ralentización
    public float effectDuration = 2f;   // Duración de la ralentización

}
