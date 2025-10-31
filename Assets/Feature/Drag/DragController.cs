using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragController : MonoBehaviour
{
    [SerializeField] private RectTransform _dragLayer;
    [SerializeField] private GraphicRaycaster _raycaster;

    private readonly UiRaycast _uiRaycast = new UiRaycast();
    private readonly DragSession _session = new DragSession();

    private void Update()
    {
        if (!_session.IsActive && Input.GetMouseButtonDown(0))
            TryStart();

        if (_session.IsActive && Input.GetMouseButton(0))
            _session.Update(Input.mousePosition);

        if (_session.IsActive && Input.GetMouseButtonUp(0))
            TryEnd();
    }

    private void TryStart()
    {
        CardView card = _uiRaycast.FindCard(_raycaster, Input.mousePosition);
        if (card == null)
            return;

        PileBase pile = card.CurrentPile;
        if (pile == null)
            return;

        if (!pile.CanPickup(card))
            return;

        List<CardView> group = pile.PickupGroup(card);
        if (group == null|| group.Count == 0)
            return;

        _session.Begin(pile, group, _dragLayer, Input.mousePosition);
    }

    private void TryEnd()
    {
        PileBase target = _uiRaycast.FindPile(_raycaster, Input.mousePosition);
        _session.End(target);
    }
}
