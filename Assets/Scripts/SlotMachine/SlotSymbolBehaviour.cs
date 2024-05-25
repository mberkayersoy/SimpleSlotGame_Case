using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class SlotSymbolBehaviour : MonoBehaviour
{
    [SerializeField] private Sprite _sharpSprite;
    [SerializeField] private Sprite _blurSprite;
    [SerializeField] private int _symbolID;
    private Transform _bottomTarget;
    private Transform _upperTarget;
    private SpriteRenderer _spriteRenderer;
    private Tween _spinTween;
    private bool _isSpinning;
    private float _tweenTimeScale;
    public float _targetY;
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
        _spinTween = null;
        _spinTween.Kill(true);
        _isSpinning = false;
        UpdateSymbolImage(_isSpinning);
        _tweenTimeScale = 1f;
    }
    public async UniTaskVoid SetSlowDown(float targetTimeScale, float slowDownDuration)
    {
        _isSpinning = false;
        UpdateSymbolImage(_isSpinning);
        float startFactor = 1f;
        float duration = slowDownDuration;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            _tweenTimeScale = Mathf.Lerp(startFactor, targetTimeScale, elapsed / duration);
            if (_spinTween != null)
            {
                _spinTween.timeScale = _tweenTimeScale;
            }
            elapsed += Time.deltaTime;
            await UniTask.Yield(PlayerLoopTiming.Update);
        }

        _tweenTimeScale = targetTimeScale;
        if (_spinTween != null)
        {
            _spinTween.timeScale = _tweenTimeScale;
        }
    }
    public void ControlMovement(float duration, bool isSpinning)
    {
        _isSpinning = isSpinning;
        UpdateSymbolImage(_isSpinning);
        ControlAnimation(duration);
    }
    private void ControlAnimation(float duration)
    {
        float targetPos = transform.position.y - _spriteRenderer.bounds.size.y;
        _targetY = targetPos;

        _spinTween = transform.DOMoveY(targetPos, duration)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                if (transform.position.y < _bottomTarget.transform.position.y)
                {   // Reset position and continue spinning
                    float newYPosition = _upperTarget.position.y;
                    transform.position = new Vector2(transform.position.x, newYPosition);
                }
                if (_spinTween != null)
                {
                    ControlAnimation(duration);
                }
            });

        _spinTween.timeScale = _isSpinning ? 1f : _tweenTimeScale;
    }
}

