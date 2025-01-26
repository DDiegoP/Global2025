using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private bool _mobileInput;

    InputData _data;

    [SerializeField]
    Vector2 _minigameSize = new Vector2(958, 539);

    // Update is called once per frame
    void Update()
    {
        _mobileInput = InputServerManager.Instance.HasClients();
        _data = InputServerManager.Instance.GetInputData();
    }

    public Vector2 GetPointerPosition() 
    {
        if(_mobileInput) return new Vector2(_data.mouse_pos.x * _minigameSize.x, _data.mouse_pos.y * _minigameSize.y);
        else return Mouse.current.position.ReadValue();
    }

    public void UpdatePos(Transform tr)
    {
        if (_mobileInput) tr.localPosition = new Vector2(_data.mouse_pos.x * _minigameSize.x, _data.mouse_pos.y * _minigameSize.y);
        else tr.position = Mouse.current.position.ReadValue();
    }

    public bool GetShake() 
    {
        if (_mobileInput) return false;
        else return Mouse.current.leftButton.wasPressedThisFrame;
    }

    public bool GetPop()
    {
        if (_mobileInput) return _data.justClicked;
        else return Mouse.current.leftButton.wasPressedThisFrame;
    }

    public bool GetPressed()
    {
        if (_mobileInput) return _data.clicked;
        else return Mouse.current.leftButton.isPressed;
    }

    public bool GetRelease()
    {
        if (_mobileInput) return _data.justReleased;
        else return Mouse.current.leftButton.wasReleasedThisFrame;
    }
}