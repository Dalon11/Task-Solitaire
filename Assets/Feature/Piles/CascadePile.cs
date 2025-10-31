using System.Collections.Generic;
using UnityEngine;

public class CascadePile : PileBase
{
    [SerializeField] private float _verticalSpacingRatio = 0.1f;
     
    public override bool CanDrop(List<CardView> group)
    {
        if (group == null)
            return false;

        if (group.Count <= 0)
            return false;

        return true;
    }

    public override void DropGroup(List<CardView> group) => AppendGroup(group);

    public override void Layout()
    {
        NormalizeSiblingOrder();
        float step = 0f;
        if (_cards.Count > 0)
        {
            Vector2 size = _cards[0].GetSize();
            step = size.y * _verticalSpacingRatio;
        }

        for (int i = 0; i < _cards.Count; i++)
        {
            Vector2 position = new Vector2(0f, -step * i);
            _cards[i].SetAnchoredPosition(position);
        }
    }

    public override float GetDragStepPixels(CardView referenceCard)
    {
        if (referenceCard == null)
            return 0f;

        Vector2 size = referenceCard.GetSize();
        return size.y * _verticalSpacingRatio;
    }
}
