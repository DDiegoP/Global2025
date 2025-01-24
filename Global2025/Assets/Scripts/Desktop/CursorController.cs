using UnityEngine;

public class CursorController : MonoBehaviour
{
    private bool _click;

    [SerializeField]
    private Transform _mouseTransform;

    public Transform mouseTransform { get { return _mouseTransform; } }

    public void Click()
    {
        _click = true;
    }

    public bool IsClickPressed()
    {
        return _click;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _click = false;
    }

    private void LateUpdate()
    {
        _click = false;
    }
}
