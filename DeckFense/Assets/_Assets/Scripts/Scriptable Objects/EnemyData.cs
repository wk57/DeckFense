using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyData", menuName = "Deckfense/Enemy Data")]
public class EnemyData : ScriptableObject
{
    public string enemyName = "Nuevo Enemigo";
    public float health = 100f;
    public float speed = 1f;
    public float damageToBase = 10f;
    public int scoreValue = 10;

    public Sprite enemySprite;   
    public GameObject enemyPrefab;
}
