using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(RecordMic))]

public class SensorsInput : MonoBehaviour {
    [SerializeField]
    RectTransform canvas;
    [SerializeField]
    float max_x_angle = 45f, max_y_value = 0.33f;

    RecordMic microphone;
    MicrophoneInput micInput;

    Vector3 attitude_reference, accelerometer_reference;

    InputData inputData;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        inputData = new InputData();
        inputData.mouse_pos_x = 0f;
        inputData.mouse_pos_y = 0f;
        inputData.clicked = false;
        inputData.microphone_loudness = 0f;
        microphone = GetComponent<RecordMic>();
        microphone.init();
        micInput = new MicrophoneInput();
        micInput.StartReading();
        InputSystem.EnableDevice(AttitudeSensor.current);
        InputSystem.EnableDevice(Accelerometer.current);
        InputSystem.EnableDevice(StepCounter.current);

        attitude_reference = AttitudeSensor.current.attitude.value.eulerAngles;
        accelerometer_reference = Accelerometer.current.acceleration.value;
    }

    private void OnDestroy() {
        micInput.EndReading();
        InputSystem.DisableDevice(StepCounter.current);
        InputSystem.DisableDevice(Accelerometer.current);
        InputSystem.DisableDevice(AttitudeSensor.current);
    }

    public void SetAsReference() {
        attitude_reference = AttitudeSensor.current.attitude.value.eulerAngles;
        accelerometer_reference = Accelerometer.current.acceleration.value;
    }

    float floatModule(float a, float b) {
        return a - Mathf.Floor(a / b) * b;
    }

    float NormalizeAttitude(float atti, float atti_reference, float max_angle) {
        return Mathf.Clamp((floatModule(atti - atti_reference + 180f, 360f) - 180f) / max_angle, -1f, 1f);
    }

    float NormalizeAccelerometer(float accel, float accel_reference, float max_value) {
        return Mathf.Clamp(-(accel - accel_reference) / max_value, -1f, 1f);
    }
    
    void ClickButton() {
        inputData.clicked = true;
    }

    void GetInput() {
        Vector3 atti = AttitudeSensor.current.attitude.value.eulerAngles;
        Debug.Log(Accelerometer.current.acceleration.value);
        Vector3 accel = Accelerometer.current.acceleration.value;
        inputData.mouse_pos_x = -NormalizeAttitude(atti.z, attitude_reference.z, max_x_angle);
        inputData.mouse_pos_y = NormalizeAccelerometer(accel.y, accelerometer_reference.y, max_y_value);
        inputData.microphone_loudness = microphone.GetLoudness();
    }

    void SendInputData(InputData input) {
        // TO DO

        // VV Temp VV

        //Debug.Log(inputData.microphone_loudness);
        //Debug.Log(input.microphone_loudness);
        RectTransform trans = GetComponent<RectTransform>();
        trans.anchoredPosition = new Vector3((canvas.sizeDelta.x / 2f) * input.mouse_pos_x, (canvas.sizeDelta.y / 2f) * input.mouse_pos_y, transform.position.z);
    }

    // Update is called once per frame
    void Update() {
        GetInput();
    }

    private void LateUpdate() {
        SendInputData(inputData);
        inputData.clicked = false;
    }
}
