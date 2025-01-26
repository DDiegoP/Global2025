using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputServerManager : MonoBehaviour
{
    public static InputServerManager Instance { get { return _instance; } }
    private static InputServerManager _instance = null;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else Destroy(this.gameObject);
    }
    
    private InputData _inputData;

    public InputData GetInputData() { return _inputData; }

    int _numClients = 0;

    public bool HasClients() { return _numClients > 0; }

    public void AddClient()
    {
        _numClients++;
    }

    public void RemoveClient()
    {
        _numClients--;
    }

    private void Start()
    {
        _inputData = new InputData();
        _inputData.mouse_pos_x = 0f;
        _inputData.mouse_pos_y = 0f;
        _inputData.clicked = false;
        _inputData.microphone_loudness = 0f;
        
        if(AttitudeSensor.current != null) attitude_reference = AttitudeSensor.current.attitude.value.eulerAngles;
        if(Accelerometer.current != null) accelerometer_reference = Accelerometer.current.acceleration.value;
    }


    [SerializeField]
    float max_x_angle = 45f, max_y_value = 0.4f;

    Vector3 attitude_reference;
    Vector3 accelerometer_reference;

    float floatModule(float a, float b)
    {
        return a - Mathf.Floor(a / b) * b;
    }

    float NormalizeAttitude(float atti, float atti_reference, float max_angle)
    {
        return Mathf.Clamp((floatModule(atti - atti_reference + 180f, 360f) - 180f) / max_angle, -1f, 1f);
    }
    float NormalizeAccelerometer(float accel, float accel_reference, float max_value) {
        return Mathf.Clamp(-(accel - accel_reference) / max_value, -1f, 1f);
    }

    public void CalculateMouseX(Vector3 atti)
    {
        _inputData.mouse_pos_x = -NormalizeAttitude(atti.z, attitude_reference.z, max_x_angle);
    }
    public void CalculateMouseY(Vector3 accel)
    {
        _inputData.mouse_pos_y = NormalizeAccelerometer(accel.y, accelerometer_reference.y, max_y_value);
    }

    public void UpdateReference(Vector3 atti)
    {
        attitude_reference = atti;
    }
    public void UpdateAccelReference(Vector3 accel)
    {
        accelerometer_reference = accel;
    }

    public void UpdatePressingScreen(bool pressing)
    {
        _inputData.justReleased = pressing && !_inputData.clicked;
        _inputData.justClicked = !pressing && _inputData.clicked;
        _inputData.clicked = pressing;
    }

    private void LateUpdate()
    {
        _inputData.justReleased = false;
        _inputData.justClicked = false;
    }
}
