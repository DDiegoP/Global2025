using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class WindowController : MonoBehaviour
{
    private bool _isDragging, _isScaling;
    private float _startScale;
    private int _corner;
    private Vector3 _offset;
    private Vector2 _scalePosition;
    private RectTransform _rectTransform;
    private WindowManager _windowManager;
    private BoxCollider2D _boxCollider;
    private Canvas _canvas;

    [SerializeField]
    private RectTransform _rectTransformBar, _leftCorner, _rightCorner;
    [SerializeField]
    private MinigameManager _gameManager;
    [SerializeField]
    private float _maxScale = 1.5f, _minScale = 0.67f, _scaleFactor = 0.001f;
    private float _barHalfHeight, _cornerHalfSize;
    private void Awake()
    {
        _windowManager = GetComponentInParent<WindowManager>();
        if(!_gameManager) _gameManager = GetComponentInChildren<MinigameManager>();
        _canvas = GetComponent<Canvas>();
        _rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        _isDragging = false;
        _boxCollider = GetComponent<BoxCollider2D>();
        _barHalfHeight = _rectTransformBar.sizeDelta.y / 2;
        _cornerHalfSize = _leftCorner.sizeDelta.y / 2;
    }

    private void OnEnable()
    {
        _windowManager.AddController(this);
    }

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame && CheckMouseOnWindow())
        {
            Debug.Log("He colisionado con la ventana");
            if (CheckMouseOnBar())
            {
                Debug.Log("He colisionado con barra");
                _isDragging = true;
                _offset = _rectTransform.position - (Vector3)Mouse.current.position.ReadValue();
            }
            else if ((_corner = CheckMouseOnCorner()) > 0)
            {
                _scalePosition = Mouse.current.position.ReadValue();
                _startScale = _rectTransform.localScale.x;
                _isScaling = true;
            }
            _windowManager.UpdateController(this);
        }
        else if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            _isDragging = false;
            _isScaling = false;
        }
        if (_isDragging)
        {
            _rectTransform.position = (Vector3)Mouse.current.position.ReadValue() + _offset;
        }
        if (_isScaling)
        {
            Scale();
        }
    }

    private bool CheckMouseOnWindow()
    {
        RaycastHit2D hit = Physics2D.Raycast(Mouse.current.position.ReadValue(), Camera.main.transform.forward);
        return hit.collider == _boxCollider;
    }

    private bool CheckMouseOnBar()
    {
        return Mouse.current.position.ReadValue().y >= _rectTransformBar.position.y - _barHalfHeight;
    }

    private int CheckMouseOnCorner() 
    {
        if (Mouse.current.position.ReadValue().y <= _leftCorner.position.y + _cornerHalfSize)
        {
            if (Mouse.current.position.ReadValue().x <= _leftCorner.position.x + _cornerHalfSize)
                return 1;
            else if (Mouse.current.position.ReadValue().x >= _rightCorner.position.x - _cornerHalfSize)
                return 2;
        }
        return 0;
    }

    private void Scale()
    {
        Vector2 dir = Mouse.current.position.ReadValue() - _scalePosition;
        if (_corner == 1) dir.x = -dir.x;
        float targetScale = _startScale * (1 + (dir.x - dir.y) * _scaleFactor);
        targetScale = Mathf.Clamp(targetScale, _minScale, _maxScale);
        _rectTransform.localScale = new Vector3(targetScale, targetScale, targetScale);
    }

    public void CloseWindow()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        _isDragging = false;
        _windowManager.RemoveController(this);
    }

    public void SetOrder(int order, bool hasFocus)
    {
        _canvas.sortingOrder = order;
        _rectTransform.position = new Vector3(_rectTransform.position.x, _rectTransform.position.y, -order);
        _gameManager.enabled = hasFocus;
    }
}
