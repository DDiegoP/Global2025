using FMODUnity;
using UnityEngine;

public class BalloonComponent : GameComponent
{
    private double _air = 0.0;

    private double _scale = 1.0;

    private bool _blowing = false;

    private ScoreComponent _scoreComponent;
    private StudioEventEmitter _emitter;


    [SerializeField]
    private float _maxScale = 4;

    private void Pop()
    {
        _blowing = false;
        // Sonido de explosion
        _emitter.Play();
    }

    private void ApplyScore()
    {
        _scoreComponent.changeScore((int) _scale);
    }

    private void Start()
    {
       _scoreComponent = GetComponentInParent<ScoreComponent>();
        _emitter = GetComponent<StudioEventEmitter>();
    }

    // Update is called once per frame
    void Update()
    {
        // Se pilla el input de soplido y se aumenta el "aire" del globo
        // if (!blowing) VER SI EMPIEZA A SOPLAR

        if (_blowing)
        {
            // Aumentamos el aire del globo (y con ello la escala)
            _air += 0.05;
            _scale += _air;
            if (_scale > _maxScale) 
                Pop();
            // Si deja de soplar ->
            // if (Input.notBlowing())
            // blowing = false; ApplyScore();
        }
    }
}
