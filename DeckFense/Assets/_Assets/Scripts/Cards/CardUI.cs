using UnityEngine;
using UnityEngine.UI;

public class CardUI : MonoBehaviour
{
    public Image iconImage;
    public Button button;
    public CardData cardData;

    public Slider cooldownSlider; 
    private float cooldownEndTime;


    private void Start()
    {
        button.onClick.AddListener(OnCardClicked);
        UpdateUI();
    }

    void Update()
    {
        if (DeckManager.Instance.IsCardOnCooldown(cardData))
        {
            float remaining = DeckManager.Instance.GetRemainingCooldown(cardData);
            cooldownSlider.value = 1f - (remaining / cardData.cooldownTime);
        }
        else
        {
            cooldownSlider.value = 1f;
        }
    }


    public void SetCard(CardData data)
    {
        cardData = data;
        UpdateUI();
        cooldownEndTime = 0f;
        cooldownSlider.value = 0f;

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
        if (!DeckManager.Instance.IsCardOnCooldown(cardData))
        {
            DeckManager.Instance.SelectCard(cardData);
            cooldownEndTime = Time.time + cardData.cooldownTime;
        }
    }
}
