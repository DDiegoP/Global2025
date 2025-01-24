using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class WindowController : MonoBehaviour
{
    private bool _isDragging;
    private Vector3 _offset;
    private RectTransform _rectTransform;
    private WindowManager _windowManager;
    private BoxCollider2D _boxCollider;
    private Canvas _canvas;

    [SerializeField]
    private RectTransform _rectTransformBar;

    private Vector2 _barHalfSize;
    private void Awake()
    {
        _windowManager = GetComponentInParent<WindowManager>();
        _canvas = GetComponent<Canvas>();
    }
    private void Start()
    {
        _isDragging = false;
        _rectTransform = GetComponent<RectTransform>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _barHalfSize = new Vector2(_rectTransformBar.sizeDelta.x / 2, _rectTransformBar.sizeDelta.y / 2);
    }


    private void OnEnable()
    {
        _windowManager.AddController(this);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && CheckMouseOnWindow())
        {
            Debug.Log("He colisionado con la ventana");
            if (CheckMouseOnBar())
            {
                Debug.Log("He colisionado con barra");
                _isDragging = true;
                _offset = _rectTransform.position - Input.mousePosition;
            }
            _windowManager.UpdateController(this);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _isDragging = false;
        }
        if (_isDragging)
        {
            _rectTransform.position = Input.mousePosition + _offset;
        }
    }

    private bool CheckMouseOnWindow()
    {
        RaycastHit2D hit = Physics2D.Raycast(Input.mousePosition, Camera.main.transform.forward);
        return hit.collider == _boxCollider;
    }

    private bool CheckMouseOnBar()
    {
        return Input.mousePosition.x >= _rectTransformBar.position.x - _barHalfSize.x && Input.mousePosition.x < _rectTransformBar.position.x + _barHalfSize.x
                && Input.mousePosition.y >= _rectTransformBar.position.y - _barHalfSize.y && Input.mousePosition.y < _rectTransformBar.position.y + _barHalfSize.y;
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

    public void SetOrder(int order)
    {
        _canvas.sortingOrder = order;
    }
}
