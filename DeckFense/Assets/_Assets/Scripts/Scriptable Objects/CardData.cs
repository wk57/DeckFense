using UnityEngine;

public enum CardEffectType { DamageArea, SlowArea }

[CreateAssetMenu(fileName = "NewCard", menuName = "Cards/CardData")]
public class CardData : ScriptableObject
{
    public string cardName;
    public Sprite icon;
    public CardEffectType effectType;
    public float effectValue;      // da�o o porcentaje de ralentizaci�n
    public float effectRadius;
    public float cooldown;
    public float cooldownTime = 3f;

}
