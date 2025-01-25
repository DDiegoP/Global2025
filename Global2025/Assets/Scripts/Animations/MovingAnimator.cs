using Unity.VisualScripting;
using UnityEngine;

public class MovingAnimator: GameComponent
{
     private float _posIzq;
    private float _posDer;
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private float _speed;

    private int _dir = 1;
    private Rigidbody2D _rb;
    private Transform _transform;

    private void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        _rb = this.GetComponent<Rigidbody2D>();
        _transform = this.transform;
    }

    private void Update()
    {
        _rb.linearVelocity = new Vector2(_dir * _speed, 0);
        _posDer = _rectTransform.position.x + _rectTransform.rect.width / 2;
        _posIzq = _rectTransform.position.x - _rectTransform.rect.width / 2;

        if ((_dir > 0 && _transform.position.x >= _posDer) || (_dir < 0 && _transform.position.x <= _posIzq))
        {
            _dir *= -1;
            _transform.localScale = new Vector3(-_transform.localScale.x, _transform.localScale.y, _transform.localScale.z);
        }
    }
}
