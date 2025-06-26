using UnityEngine;
using UnityEngine.UI;

public class CardUI : MonoBehaviour
{
    public Image iconImage;
    public Button button;
    public CardData cardData;

    private void Start()
    {
        button.onClick.AddListener(OnCardClicked);
        UpdateUI();
    }

    public void SetCard(CardData data)
    {
        cardData = data;
        UpdateUI();
    }

    void UpdateUI()
    {
        if (cardData != null)
        {
            iconImage.sprite = cardData.icon;
            // puedes agregar texto si quieres mostrar el nombre
        }
    }

    void OnCardClicked()
    {
        DeckManager.Instance.SelectCard(cardData);
    }
}
