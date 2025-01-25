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

    private void Start()
    {
        _inputData = new InputData();
        _inputData.mouse_pos = Vector2.zero;
        _inputData.mouse_rotation = 0f;
        _inputData.clicked = false;
        _inputData.microphone_loudness = 0f;
        
        if(AttitudeSensor.current != null) attitude_reference = AttitudeSensor.current.attitude.value.eulerAngles;
    }

    [SerializeField]
    TextMeshProUGUI _text;

    [SerializeField]
    GameObject _updatePanel;

    [SerializeField]
    float max_x_angle = 45f, max_y_angle = 30f;

    Vector3 attitude_reference;

    Vector2 GetMousePositionFromSensors(Vector3 atti)
    {
        return new Vector2(-NormalizeAttitude(atti.z, attitude_reference.z, max_x_angle), NormalizeAttitude(atti.x, attitude_reference.x, max_y_angle));
    }

    float floatModule(float a, float b)
    {
        return a - Mathf.Floor(a / b) * b;
    }

    float NormalizeAttitude(float atti, float atti_reference, float max_angle)
    {
        return Mathf.Clamp((floatModule(atti - atti_reference + 180f, 360f) - 180f) / max_angle, -1f, 1f);
    }

    float GetMouseRotationFromSensors(Vector3 atti)
    {
        return atti.z - attitude_reference.z;
    }

    public void CalculateInput(Vector3 atti)
    {
        _text.text = atti.ToString();

        _inputData.mouse_pos = GetMousePositionFromSensors(atti);
        _inputData.mouse_rotation = GetMouseRotationFromSensors(atti);

        // RELLENAR MICROFONO :)


        // _inputData.microphone_loudness = microphone.GetLoudness();
    }

    public void UpdateReference(Vector3 reference)
    {
        attitude_reference = reference;
        _updatePanel.SetActive(!_updatePanel.activeSelf);
    }
}
