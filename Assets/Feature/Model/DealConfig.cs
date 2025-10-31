using UnityEngine;

[CreateAssetMenu(fileName = nameof(DealConfig), menuName = "Solitaire/" + nameof(DealConfig))]
public class DealConfig : ScriptableObject
{
    [SerializeField] private DeckModel _deckModel;
    [SerializeField] private CascadeLayoutModel _cascadeLayout;
    [SerializeField] private CardView _cardPrefab;
    [SerializeField] private Sprite _cardBackSprite;

    public DeckModel DeckModel => _deckModel;
    public CascadeLayoutModel CascadeLayout => _cascadeLayout;
    public CardView CardPrefab => _cardPrefab;
    public Sprite CardBackSprite => _cardBackSprite;
}
