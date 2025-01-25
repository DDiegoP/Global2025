using UnityEngine;
using UnityEngine.InputSystem;

public class HandCursorComponent : MonoBehaviour
{
    private BubbleComponent _currentBubble = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _currentBubble = collision.GetComponent<BubbleComponent>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _currentBubble = null;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Input.mousePosition;
        if (_currentBubble != null && Mouse.current.leftButton.wasReleasedThisFrame)
        {
            _currentBubble.Pop();
        }
    }
}
