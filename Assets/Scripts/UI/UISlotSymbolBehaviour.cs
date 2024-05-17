using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UISlotSymbolBehaviour : MonoBehaviour
{
    [SerializeField] private Sprite _sharpSprite;
    [SerializeField] private Sprite _blurSprite;
    [SerializeField] private int _symbolID;
    public float _targetY;
    private Image _image;
    private RectTransform _parentRectTransform;
    private RectTransform _rectTransform;
    private Tween _spinTween;
    private bool _slowDown;
    private float _tweenTimeFactor = 0.1f;
    public int SymbolID { get => _symbolID; private set => _symbolID = value; }
    public Tween SpinTween { get => _spinTween; set => _spinTween = value; }
    public float TargetY { get => _targetY; set => _targetY = value; }

    private void Awake()
    {
        _image = GetComponent<Image>();
        _parentRectTransform = transform.parent.GetComponent<RectTransform>();
        _rectTransform = GetComponent<RectTransform>();
    }
    public void Initialize(SlotSymbolData slotSymbolData)
    {
        _symbolID = slotSymbolData.ID;
        _sharpSprite = Resources.Load<Sprite>(slotSymbolData.SharpSpritePath);
        _blurSprite = Resources.Load<Sprite>(slotSymbolData.BlurSpritePath);
        UpdateSymbolImage(false);
    }
    private void UpdateSymbolImage(bool isSpinning)
    {
        _image.sprite = isSpinning ? _blurSprite : _sharpSprite;
    }
    public void StopMovement()
    {
        _image.sprite = _sharpSprite;
        _spinTween = null;
        _spinTween.Kill(true);
        _slowDown = false;
    }

    public void SlowDownTween(float tweenTimeFactor)
    {
        _slowDown = true;
        _tweenTimeFactor = tweenTimeFactor;
        UpdateSymbolImage(!_slowDown);
    }
    public void ControlMovement(float duration, bool isSpinning)
    {
        UpdateSymbolImage(isSpinning);
        ControlAnimation(duration, isSpinning);
    }
    private void ControlAnimation(float duration, bool isSpinning = true)
    {
        float endYPosition = _rectTransform.rect.yMin;
        float targetPos = _rectTransform.anchoredPosition.y - _rectTransform.rect.size.y;
        _targetY = targetPos;

        _spinTween = _rectTransform.DOAnchorPosY(targetPos, duration)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                if (_rectTransform.anchoredPosition.y <= _parentRectTransform.rect.yMin)
                {   // Reset position and continue spinning
                    float newYPosition = _parentRectTransform.rect.yMax - _rectTransform.rect.height / 2;
                    _rectTransform.anchoredPosition = new Vector2(_rectTransform.anchoredPosition.x, newYPosition);
                }
                if (_spinTween != null)
                {
                    ControlAnimation(duration);
                }
            });

        _spinTween.timeScale = _slowDown ? _tweenTimeFactor : 1f;
    }
}

