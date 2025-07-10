using UnityEngine;

public class CardEffectHandler : MonoBehaviour
{
    public LayerMask groundLayer;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Clic izquierdo
        {
            CardData selectedCard = DeckManager.Instance.GetSelectedCard();
            if (selectedCard == null) return;
            DeckManager.Instance.PutCardOnCooldown(selectedCard);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100f, groundLayer))
            {
                Vector3 targetPos = hit.point;
                ActivateEffect(selectedCard, targetPos);
                DeckManager.Instance.ClearSelectedCard();
            }
        }
    }

    void ActivateEffect(CardData cardData, Vector3 position)
    {
        Debug.Log("Lanzando carta: " + cardData.cardName + " en " + position);

        // Efecto visual único por carta
        if (cardData.visualEffectPrefab != null)
            Instantiate(cardData.visualEffectPrefab, position, Quaternion.identity);

        // Aplicar efecto a los enemigos en el área
        Collider[] hits = Physics.OverlapSphere(position, cardData.effectRadius);
        foreach (var hit in hits)
        {
            EnemyController enemy = hit.GetComponent<EnemyController>();
            if (enemy != null)
            {
                switch (cardData.effectType)
                {
                    case CardEffectType.DamageArea:
                        enemy.TakeDamage(cardData.effectValue);
                        break;

                    case CardEffectType.SlowArea:
                        enemy.ApplySlow(cardData.slowFactor, cardData.effectDuration);
                        break;

                    case CardEffectType.SlowAndDamageArea:
                        enemy.TakeDamage(cardData.effectValue);
                        enemy.ApplySlow(cardData.slowFactor, cardData.effectDuration);
                        break;
                }
            }
        }
    }
}
