using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISlotSymbolBehaviour : MonoBehaviour
{
    [SerializeField] private Sprite _sharpSprite;
    [SerializeField] private Sprite _blurSprite;
    [SerializeField] private int _symbolID;
    [SerializeField] private float _speed;
    private Image _image;
    private RectTransform _parentRectTransform;
    private RectTransform _rectTransform;
    public int SymbolID { get => _symbolID; private set => _symbolID = value; }

    private void Awake()
    {
        _image = GetComponent<Image>();
        _parentRectTransform = transform.parent.GetComponent<RectTransform>();
        _rectTransform = GetComponent<RectTransform>();
    }

    public void Initialize(int symbolID, Sprite sharp, Sprite blur, float speed)
    {
        _symbolID = symbolID;
        _sharpSprite = sharp;
        _blurSprite = blur;
        _speed = speed;
    }
    private void UpdateSymbolImage()
    {
        _image.sprite = _blurSprite;
    }

    private void FixedUpdate()
    {
        _rectTransform.position += Vector3.down * Time.fixedDeltaTime * _speed;

        if (_rectTransform.anchoredPosition.y <= _parentRectTransform.rect.yMin)
        {
            float newYPosition = _parentRectTransform.rect.yMax; //- _rectTransform.rect.height / 2;
            _rectTransform.anchoredPosition = new Vector2(_rectTransform.anchoredPosition.x, newYPosition);
        }
        
    }
}
