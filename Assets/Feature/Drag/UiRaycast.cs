using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UiRaycast
{
    private readonly List<RaycastResult> _buf = new List<RaycastResult>();

    public CardView FindCard(GraphicRaycaster raycaster, Vector2 screenPos)
    {
        Raycast(raycaster, screenPos);
        for (int i = 0; i < _buf.Count; i++)
        {
            GameObject card = _buf[i].gameObject;
            if (card == null)
                continue;

            CardView cardView = card.GetComponentInParent<CardView>();
            if (cardView != null)
                return cardView;
        }

        return null;
    }

    public PileBase FindPile(GraphicRaycaster raycaster, Vector2 screenPos)
    {
        Raycast(raycaster, screenPos);
        for (int i = 0; i < _buf.Count; i++)
        {
            GameObject card = _buf[i].gameObject;
            if (card == null)
                continue;

            CardView cardView = card.GetComponentInParent<CardView>();
            if (cardView != null)
                return cardView.CurrentPile;

            PileBase pile = card.GetComponentInParent<PileBase>();
            if (pile != null)
                return pile;
        }

        return null;
    }

    private void Raycast(GraphicRaycaster raycaster, Vector2 screenPos)
    {
        _buf.Clear();
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = screenPos;
        raycaster.Raycast(eventData, _buf);
    }
}
