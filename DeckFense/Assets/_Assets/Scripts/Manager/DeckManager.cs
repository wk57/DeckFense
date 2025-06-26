using UnityEngine;
using System.Collections.Generic;


public class DeckManager : MonoBehaviour
{
    public static DeckManager Instance;

    public List<CardData> startingDeck; // Arrástralas desde el editor
    public GameObject cardUIPrefab;
    public Transform handPanel; // Un layout donde se mostrarán las cartas (Horizontal Layout Group)

    public CardData selectedCard;

    private Dictionary<CardData, float> cardCooldowns = new Dictionary<CardData, float>();


    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        DrawStartingHand();
    }

    void DrawStartingHand()
    {
        foreach (CardData card in startingDeck)
        {
            GameObject cardUI = Instantiate(cardUIPrefab, handPanel);
            cardUI.GetComponent<CardUI>().SetCard(card);
        }
    }

    public void SelectCard(CardData card)
    {
        selectedCard = card;
        Debug.Log("Carta seleccionada: " + card.cardName);
    }

    public CardData GetSelectedCard()
    {
        return selectedCard;
    }

    public void ClearSelectedCard()
    {
        selectedCard = null;
    }

    public bool IsCardOnCooldown(CardData card)
    {
        return cardCooldowns.ContainsKey(card) && Time.time < cardCooldowns[card];
    }

    public void PutCardOnCooldown(CardData card)
    {
        cardCooldowns[card] = Time.time + card.cooldownTime;
    }

    public float GetRemainingCooldown(CardData card)
    {
        if (!cardCooldowns.ContainsKey(card)) return 0f;
        return Mathf.Max(0f, cardCooldowns[card] - Time.time);
    }


}
