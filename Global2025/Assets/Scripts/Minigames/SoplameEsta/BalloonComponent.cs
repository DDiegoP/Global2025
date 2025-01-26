using FMODUnity;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class BalloonComponent : GameComponent
{

    private bool _blowing = false;
    private RectTransform _buble;
    private ScoreComponent _scoreComponent;
    private StudioEventEmitter _emitter;
    private Animator _animator;
    [SerializeField]
    private Slider _slider;
    [SerializeField]
    private GameObject _bubbleFloat;


    [SerializeField]
    private float _maxScale = 4, _scaleFactor = 5.00f;

    private void Pop()
    {
        _blowing = false;
        _animator.Play("BubblePop");
        _emitter.Play();
    }

    private void ApplyScore()
    {
        _scoreComponent.changeScore(Mathf.Round((Mathf.Pow(2f, _buble.localScale.x-1) - 1) * 10));
    }

    private void ResetBubble()
    {
        _buble.localScale = new Vector3(1,1,1);
        _buble.localPosition = new Vector3(_buble.localPosition.x, 46.5f * _buble.localScale.x - 174.5f, _buble.localPosition.z);
        _slider.value = _slider.minValue;
    }

    private void SuccesfulBubble()
    {
        Instantiate(_bubbleFloat, transform.parent);
    }

    InputManager _input;

    private void Start()
    {
        _input = transform.root.GetComponent<InputManager>();
       _scoreComponent = GetComponentInParent<ScoreComponent>();
       _emitter = GetComponent<StudioEventEmitter>();
       _buble = GetComponentInParent<RectTransform>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_input.GetPop())
        {
            _blowing = true;
        }
        else if (_blowing && _input.GetRelease())
        {
            _blowing = false;
            ApplyScore();
            SuccesfulBubble();
            ResetBubble();
        }
        if (_blowing)
        {
            _buble.localScale *= (1 + _scaleFactor * Time.deltaTime);
            _buble.localPosition = new Vector3(_buble.localPosition.x, 46.5f * _buble.localScale.x - 174.5f, _buble.localPosition.z);
            _slider.value = _buble.localScale.x;
            if (_buble.localScale.x > _maxScale)
                Pop();
        }
    }
}
