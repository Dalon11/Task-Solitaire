using System.Collections.Generic;
using UnityEngine;

public class DealController : MonoBehaviour
{
    [SerializeField] private DealConfig _model;
    [SerializeField] private DeckPile _deckPile;
    [SerializeField] private List<CascadePile> _cascadePiles = new List<CascadePile>();
    [SerializeField] private List<FoundationPile> _foundationPiles = new List<FoundationPile>();

    private void Start() => DealFirst();

    private void DealFirst()
    {
        if (_model == null || _deckPile == null)
            return;

        InstantiateDeckViews();
        DealToCascadesFromLayout();
        LayoutAllPiles();
    }

    private void LayoutAllPiles()
    {
        _deckPile.Layout();

        for (int i = 0; i < _cascadePiles.Count; i++)
        {
            CascadePile pile = _cascadePiles[i];
            pile.Layout();
        }

        for (int i = 0; i < _foundationPiles.Count; i++)
        {
            FoundationPile pile = _foundationPiles[i];
            pile.Layout();
        }
    }

    private void InstantiateDeckViews()
    {
        for (int i = 0; i < _model.DeckModel.CardCount; i++)
        {
            CardView card = Instantiate(_model.CardPrefab, _deckPile.Container);
            card.Initialize(_model.CardBackSprite, _deckPile);
            _deckPile.AppendCardForSetup(card);
        }
    }

    private void DealToCascadesFromLayout()
    {
        if (_cascadePiles.Count <= 0)
            return;

        for (int pileIndex = 0; pileIndex < _cascadePiles.Count; pileIndex++)
        {
            int targetCount = _model.CascadeLayout.ColumnCardCounts[pileIndex];
            for (int i = 0; i < targetCount; i++)
            {
                CardView card = _deckPile.RemoveTopCardForSetup();

                if (card == null)
                    return;

                _cascadePiles[pileIndex].AppendCardForSetup(card);
            }
        }
    }
}