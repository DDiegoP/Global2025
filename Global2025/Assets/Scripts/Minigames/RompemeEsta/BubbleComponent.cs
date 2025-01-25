using FMODUnity;
using UnityEngine;

public class BubbleComponent : GameComponent
{
    public bool negativeBubble = false;
    private ScoreComponent _scoreComponent;
    private StudioEventEmitter _emitter;

    private void Pop()
    {
        int sign = negativeBubble ? -1 : 1;
        // Llamar al score component
        _scoreComponent.changeScore(sign * 5);
        // Sonido de explotar
        _emitter.Play();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<HandCursorComponent>() != null)
            Pop();
    }

    private void Start()
    {
        _scoreComponent = GetComponentInParent<ScoreComponent>();
        _emitter = GetComponent<StudioEventEmitter>();
    }
}
