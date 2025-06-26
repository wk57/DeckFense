using UnityEngine;

public enum CardEffectType { DamageArea, SlowArea }

[CreateAssetMenu(fileName = "NewCard", menuName = "Cards/CardData")]
public class CardData : ScriptableObject
{
    public string cardName;
    public Sprite icon;
    public CardEffectType effectType;
    public float effectValue;      // daño o porcentaje de ralentización
    public float effectRadius;
    public float cooldown;
    public float cooldownTime = 3f;

}
