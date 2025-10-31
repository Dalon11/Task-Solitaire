using System.Collections.Generic;
using UnityEngine;

public class FoundationPile : PileBase
{
    public override bool CanPickup(CardView card)
    {
        if (!base.CanPickup(card))
            return false;

        int lastIndex = _cards.Count - 1;

        if (lastIndex < 0)
            return false;

        return _cards[lastIndex] == card;
    }

    public override List<CardView> PickupGroup(CardView card)
    {
        if (!CanPickup(card))
            return null;

        List<CardView> group = new List<CardView>();
        int lastIndex = _cards.Count - 1;
        group.Add(_cards[lastIndex]);
        _cards.RemoveAt(lastIndex);

        return group;
    }

    public override bool CanDrop(List<CardView> group)
    {
        if (group == null)
            return false;

        if (group.Count != 1)
            return false;

        return true;
    }

    public override void DropGroup(List<CardView> group) => AppendGroup(group);

    public override void Layout()
    {
        NormalizeSiblingOrder();
        for (int i = 0; i < _cards.Count; i++)        
            _cards[i].SetAnchoredPosition(Vector2.zero);
    }
}
