using FMODUnity;
using UnityEngine;

public class BubbleComponent : GameComponent
{
    public bool negativeBubble = false;
    private ScoreComponent _scoreComponent;
    private StudioEventEmitter _emitter;
    private Animator _animator;


    [SerializeField]
    private float _offset = 5;

    private Vector3 _nextPosition;
    private Vector3 _initialPosition;
    private Vector3 _direction;

    public void AnimatePop()
    {
        _animator.Play("BubblePositivePop");
    }

    public void Pop()
    {
        int sign = negativeBubble ? -1 : 1;
        // Llamar al score component
        _scoreComponent.changeScore(sign * 5);
        gameObject.SetActive(false);
        _emitter.Play();
    }

    private void calculateNextPosition()
    {
        _nextPosition = new Vector3(Random.Range(_initialPosition.x - _offset, _initialPosition.x + _offset),
            Random.Range(_initialPosition.y - _offset, _initialPosition.y + _offset), 0);

        _direction = (_nextPosition - transform.localPosition);
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _scoreComponent = GetComponentInParent<ScoreComponent>();
        _emitter = GetComponent<StudioEventEmitter>();
    }

    public void SetStartPosition(Vector3 position)
    {
        transform.localPosition = position;
        _initialPosition = transform.localPosition;
        calculateNextPosition();
    }

    private void Update()
    {
        if (transform.localPosition == _nextPosition)
        {
            calculateNextPosition();
        }
        transform.localPosition += _direction * Time.deltaTime;
    }
}
