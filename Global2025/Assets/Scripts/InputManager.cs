using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private bool _mobileInput;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void SetMobileInput(bool mobile)
    {
        _mobileInput = mobile;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector2 GetPointerPosition() 
    {
        if(_mobileInput) return Vector2.zero;
        else return Mouse.current.position.ReadValue();
    }

    public bool GetShake() 
    {
        if (_mobileInput) return false;
        else return Mouse.current.leftButton.wasPressedThisFrame;
    }

    public bool GetPop()
    {
        if (_mobileInput) return false;
        else return Mouse.current.leftButton.wasPressedThisFrame;
    }

    public bool GetRelease()
    {
        if (_mobileInput) return false;
        else return Mouse.current.leftButton.wasReleasedThisFrame;
    }

}
