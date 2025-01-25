using UnityEngine;

public class FoamComponent : GameComponent
{
    enum FoamState { DIRTY, HALF_DIRTY, CLEAN, ERASED}

    private FoamState st;

    [SerializeField]
    private double _cooldown = 0.75;

    private double _elapsedTime = 0.0;

    private ScoreComponent _scoreComponent;
    private Animator _animator;

    private void changeState()
    {
        _elapsedTime = 0.0;
        // Actualizamos el FoamState
        if (st < FoamState.ERASED)
        {
            st++;
            switch (st)
            {
                case FoamState.HALF_DIRTY:   break;
                case FoamState.CLEAN: break;
                case FoamState.ERASED: break;
            }
            _scoreComponent.changeScore(1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<HandCursorComponent>() != null && _elapsedTime >= _cooldown)
            changeState();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        st = FoamState.DIRTY;
        _scoreComponent = GetComponentInParent<ScoreComponent>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;
    }
}
