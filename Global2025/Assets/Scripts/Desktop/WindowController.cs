using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class WindowController : MonoBehaviour
{
    private bool _isDragging;
    private Vector3 _offset;
    private RectTransform _rectTransform;
    private WindowManager _windowManager;
    private MinigameManager _gameManager;
    private BoxCollider2D _boxCollider;
    private Canvas _canvas;

    [SerializeField]
    private RectTransform _rectTransformBar;

    private float _barHeightHalfSize;
    private void Awake()
    {
        _windowManager = GetComponentInParent<WindowManager>();
        _gameManager = GetComponentInParent<MinigameManager>();
        _canvas = GetComponent<Canvas>();
        _rectTransform = GetComponent<RectTransform>();
    }
    private void Start()
    {
        _isDragging = false;
        _boxCollider = GetComponent<BoxCollider2D>();
        _barHeightHalfSize = _rectTransformBar.sizeDelta.y / 2;
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
            _windowManager.UpdateController(this);
        }
        else if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            _isDragging = false;
        }
        if (_isDragging)
        {
            _rectTransform.position = (Vector3)Mouse.current.position.ReadValue() + _offset;
        }
    }

    private bool CheckMouseOnWindow()
    {
        RaycastHit2D hit = Physics2D.Raycast(Mouse.current.position.ReadValue(), Camera.main.transform.forward);
        return hit.collider == _boxCollider;
    }

    private bool CheckMouseOnBar()
    {
        return Mouse.current.position.ReadValue().y >= _rectTransformBar.position.y - _barHeightHalfSize;
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
