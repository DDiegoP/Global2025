using UnityEngine;

public class BubbleComponent : GameComponent
{
    public bool negativeBubble = false;

    private void Pop()
    {
        int sign = negativeBubble ? -1 : 1;
        // Llamar al score component
        // score.changeScore(sign * 5);
        // Animacion de explotar?
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<HandCursorComponent>() != null)
        {
            Pop();
        }
    }
}
