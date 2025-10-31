using System.Collections.Generic;
using UnityEngine;

public class DragSession
{
    private readonly List<CardView> _group = new List<CardView>();
    private PileBase _source;
    private RectTransform _layer;
    private float _step;
    private Vector2 _grabOffset;
    private bool _active;

    public bool IsActive =>_active;  

    public void Begin(PileBase source, List<CardView> group, RectTransform layer, Vector2 screenPos)
    {
        if (source == null)
            return;

        if (group == null || group.Count == 0)
            return;

        _active = true;
        _source = source;
        _layer = layer;
        _group.Clear();
        Vector2 cardSize = group[0].RectTransform.rect.size;
        for (int i = 0; i < group.Count; i++)
        {
            CardView cardView = group[i];
            _group.Add(cardView);
            cardView.SetRaycastBlock(false);
            cardView.SetParent(_layer, false);
            cardView.SetAbsoluteSize(cardSize);
        }

        _step = _source.GetDragStepPixels(_group[0]);

        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(_layer, screenPos, null, out Vector2 local))
            return;

        _grabOffset = Vector2.zero; //local - _group[0].RectTransform.anchoredPosition;
    }

    public void Update(Vector2 screenPos)
    {
        if (!_active)
            return;

        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(_layer, screenPos, null, out Vector2 local))
        {
            return;
        }
        
        Vector2 basePos = local - _grabOffset;
        for (int i = 0; i < _group.Count; i++)
        {
            Vector2 pos = basePos + new Vector2(0f, -_step * i);
            _group[i].SetAnchoredPosition(pos);
        }
    }

    public void End(PileBase target)
    {
        if (!_active)
            return;

        if (target == null || !target.CanDrop(_group))
            target = _source;

        target.DropGroup(_group);
        for (int i = 0; i < _group.Count; i++)
            _group[i].SetRaycastBlock(true);

        target.Layout();
        if (target != _source)
            _source.Layout();

        Reset();
    }

    private void Reset()
    {
        _group.Clear();
        _source = null;
        _layer = null;
        _step = 0f;
        _grabOffset = Vector2.zero;
        _active = false;
    }
}
