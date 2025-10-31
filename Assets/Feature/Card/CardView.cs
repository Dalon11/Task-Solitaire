
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class CardView : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private CanvasGroup _canvasGroup;

    private RectTransform _rectTransform;
    private PileBase _currentPile;

    public RectTransform RectTransform => _rectTransform;
    public PileBase CurrentPile => _currentPile;

    private void Awake() => _rectTransform = (RectTransform)transform;

    public void Initialize(Sprite cardSprite, PileBase pile)
    {
        _image.sprite = cardSprite;
        _currentPile = pile;
    }

    public void SetCurrentPile(PileBase pile) => _currentPile = pile;

    public void SetParent(Transform parentTransform, bool worldPositionStays = false) => transform.SetParent(parentTransform, worldPositionStays);

    public void SetAnchoredPosition(Vector2 position) => _rectTransform.anchoredPosition = position;

    public Vector2 GetSize() => _rectTransform.rect.size;

    public void SetRaycastBlock(bool enabledFlag)
    {
        if (_canvasGroup != null)
            _canvasGroup.blocksRaycasts = enabledFlag;
    }
    public void FillParent()
    {
        _rectTransform.anchorMin = Vector2.zero;
        _rectTransform.anchorMax = Vector2.one;
        _rectTransform.pivot = new Vector2(0.5f, 0.5f);
        _rectTransform.offsetMin = Vector2.zero;
        _rectTransform.offsetMax = Vector2.zero;
        _rectTransform.localScale = Vector3.one;
        _rectTransform.anchoredPosition = Vector2.zero;
    }
    public void SetAbsoluteSize(Vector2 size)
    {
        Vector2 vector = new Vector2(0.5f, 0.5f);
        _rectTransform.anchorMin = vector;
        _rectTransform.anchorMax = vector;
        _rectTransform.pivot = vector;
        _rectTransform.sizeDelta = size;
        _rectTransform.localScale = Vector3.one;
    }
}
