using FMODUnity;
using UnityEngine;
using UnityEngine.InputSystem;

public class BalloonComponent : GameComponent
{

    private bool _blowing = false;
    private RectTransform _buble;
    private ScoreComponent _scoreComponent;
    private StudioEventEmitter _emitter;


    [SerializeField]
    private float _maxScale = 4, _scaleFactor = 5.00f;

    private void Pop()
    {
        _blowing = false;
        //animacion explota burbuja
        ResetBubble();
        // Sonido de explosion
        //_emitter.Play();
    }

    private void ApplyScore()
    {
        _scoreComponent.changeScore(Mathf.Round((Mathf.Pow(2f, _buble.localScale.x-1) - 1) * 10));
    }

    private void ResetBubble()
    {
        _buble.localScale = new Vector3(1,1,1);
        _buble.localPosition = new Vector3(_buble.localPosition.x, 46.5f * _buble.localScale.x - 174.5f, _buble.localPosition.z);
    }

    private void Start()
    {
       _scoreComponent = GetComponentInParent<ScoreComponent>();
       //_emitter = GetComponent<StudioEventEmitter>();
       _buble = GetComponentInParent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            _blowing = true;
        }
        else if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            _blowing = false;
            ApplyScore();
            //Animacion de que suba la burbuja
            Pop();
        }
        if (_blowing)
        {
            _buble.localScale *= (1 + _scaleFactor * Time.deltaTime);
            _buble.localPosition = new Vector3(_buble.localPosition.x, 46.5f * _buble.localScale.x - 174.5f, _buble.localPosition.z);
            if (_buble.localScale.x > _maxScale)
                Pop();
        }
    }
}
