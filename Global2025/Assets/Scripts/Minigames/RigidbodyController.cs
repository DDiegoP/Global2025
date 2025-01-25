using UnityEngine;

public class RigidbodyController : GameComponent
{
    private Rigidbody2D _rb;

    void Awake()
    {
        base.Awake();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _rb.bodyType = RigidbodyType2D.Kinematic;
    }

    private void OnDisable()
    {
        _rb.bodyType = RigidbodyType2D.Static;
    }
}
