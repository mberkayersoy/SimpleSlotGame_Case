using DG.Tweening;
using UnityEngine;

public class SlotSymbolBehaviour : MonoBehaviour
{
    [SerializeField] private Sprite _sharpSprite;
    [SerializeField] private Sprite _blurSprite;
    [SerializeField] private int _symbolID;
    public float _targetY;
    private Transform _bottomTarget;
    private Transform _upperTarget;
    private SpriteRenderer _spriteRenderer;
    private Tween _spinTween;
    private bool _slowDown;
    private float _tweenTimeFactor = 0.1f;
    public int SymbolID { get => _symbolID; private set => _symbolID = value; }
    public float TargetY { get => _targetY; set => _targetY = value; }

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();

    }
    public void Initialize(SlotSymbolData slotSymbolData, Transform bottomTarget, Transform upperTarget)
    {
        _symbolID = slotSymbolData.ID;
        _sharpSprite = Resources.Load<Sprite>(slotSymbolData.SharpSpritePath);
        _blurSprite = Resources.Load<Sprite>(slotSymbolData.BlurSpritePath);
        _bottomTarget = bottomTarget;
        _upperTarget = upperTarget;
        UpdateSymbolImage(false);
    }
    private void UpdateSymbolImage(bool isSpinning)
    {
        _spriteRenderer.sprite = isSpinning ? _blurSprite : _sharpSprite;
    }
    public void StopMovement()
    {
        _spriteRenderer.sprite = _sharpSprite;
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
        ControlAnimation(duration);
    }
    private void ControlAnimation(float duration)
    {
        //float endYPosition = _spriteRenderer.bounds.size.y;
        float targetPos = transform.position.y - _spriteRenderer.bounds.size.y;
        _targetY = targetPos;

        _spinTween = transform.DOMoveY(targetPos, duration)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                if (transform.position.y < _bottomTarget.transform.position.y)
                {   // Reset position and continue spinning
                    float newYPosition = _upperTarget.position.y; //- _spriteRenderer.bounds.size.y / 2;
                    transform.position = new Vector2(transform.position.x, newYPosition);
                }
                if (_spinTween != null)
                {
                    ControlAnimation(duration);
                }
            });

        _spinTween.timeScale = _slowDown ? _tweenTimeFactor : 1f;
    }
}

