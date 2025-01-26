using UnityEngine;
using UnityEngine.InputSystem;

public class HandCursorComponent : GameComponent
{
    private BubbleComponent _currentBubble = null;

    private InputManager _input;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _currentBubble = collision.GetComponent<BubbleComponent>();
    }

    private void Start()
    {
        _input = transform.root.GetComponent<InputManager>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(_currentBubble == collision.GetComponent<BubbleComponent>()) _currentBubble = null;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = _input.GetPointerPosition();
        if (_currentBubble != null && _input.GetPop())
        {
            _currentBubble.Pop();
            _currentBubble = null;
        }
    }
}
