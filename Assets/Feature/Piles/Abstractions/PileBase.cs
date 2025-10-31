using System.Collections.Generic;
using UnityEngine;

public abstract class PileBase : MonoBehaviour
{
    private RectTransform _container;

    protected List<CardView> _cards = new List<CardView>();

    public RectTransform Container => _container;

    private void Awake() => _container = (RectTransform)transform;

    public virtual bool CanPickup(CardView card)
    {
        if (card == null)
            return false;

        int index = GetCardIndex(card);
        if (index < 0)
            return false;

        return true;
    }

    public virtual List<CardView> PickupGroup(CardView card)
    {
        int index = GetCardIndex(card);
        if (index < 0)
            return null;

        List<CardView> group = new List<CardView>();
        for (int i = index; i < _cards.Count; i++)
        {
            group.Add(_cards[i]);
        }

        _cards.RemoveRange(index, _cards.Count - index);
        return group;
    }

    public abstract bool CanDrop(List<CardView> group);

    public abstract void DropGroup(List<CardView> group);

    public abstract void Layout();

    public void AppendCardForSetup(CardView card)
    {
        if (card == null)
            return;

        _cards.Add(card);
        card.SetCurrentPile(this);
        card.SetParent(_container);
        card.FillParent();
    }

    public CardView RemoveTopCardForSetup()
    {
        if (_cards.Count <= 0)
            return null;

        int lastIndex = _cards.Count - 1;
        CardView card = _cards[lastIndex];
        _cards.RemoveAt(lastIndex);
        return card;
    }

    protected int GetCardIndex(CardView card)
    {
        for (int i = 0; i < _cards.Count; i++)
        {
            if (_cards[i] == card)
                return i;
        }

        return -1;
    }

    protected void AppendGroup(List<CardView> group)
    {
        if (group == null || group.Count <= 0)
            return;

        for (int i = 0; i < group.Count; i++)
        {
            CardView card = group[i];
            _cards.Add(card);
            card.SetCurrentPile(this);
            card.SetParent(_container);
            card.FillParent();
        }
    }
    protected void NormalizeSiblingOrder()
    {
        for (int i = 0; i < _cards.Count; i++)
            _cards[i].RectTransform.SetSiblingIndex(i);
    }

    public virtual float GetDragStepPixels(CardView referenceCard) => 0f;

    private Vector2 _lastSize;

    //private void OnRectTransformDimensionsChange()
    //{
    //    if (Container == null)
    //        return;

    //    Vector2 s = Container.rect.size;
    //    if (s == _lastSize)
    //        return;

    //    _lastSize = s;
    //    Layout();
    //}
}
