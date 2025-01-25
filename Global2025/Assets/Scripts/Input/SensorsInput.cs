using UnityEngine;
using UnityEngine.InputSystem;

using Gyroscope = UnityEngine.InputSystem.Gyroscope;

public class SensorsInput : MonoBehaviour
{
    [SerializeField]
    RectTransform canvas;
    [SerializeField]
    float max_x_angle = 45f, max_y_angle = 30f;

    Vector3 attitude_reference;

    const float no_gravity = 9.8f;

    InputData inputData;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        inputData = new InputData();
        inputData.mouse_pos = Vector2.zero;
        inputData.mouse_rotation = 0f;
        inputData.clicked = false;

        InputSystem.EnableDevice(AttitudeSensor.current);
        InputSystem.EnableDevice(StepCounter.current);

        attitude_reference = AttitudeSensor.current.attitude.value.eulerAngles;
    }

    private void OnDestroy() {
        InputSystem.DisableDevice(StepCounter.current);
        InputSystem.DisableDevice(AttitudeSensor.current);
    }

    public void SetAsReference() {
        attitude_reference = AttitudeSensor.current.attitude.value.eulerAngles;
    }

    Vector2 GetMousePositionFromSensors(Vector3 atti) {
        return new Vector2(-NormalizeAttitude(atti.z, attitude_reference.z, max_x_angle), NormalizeAttitude(atti.x, attitude_reference.x, max_y_angle));
    }

    float floatModule(float a, float b) {
        return a - Mathf.Floor(a / b) * b;
    }

    float NormalizeAttitude(float atti, float atti_reference, float max_angle) {
        return Mathf.Clamp((floatModule(atti - atti_reference + 180f, 360f) - 180f) / max_angle, -1f, 1f);
    }
    
    float GetMouseRotationFromSensors(Vector3 atti) {
        return atti.z - attitude_reference.z;
    }

    void ClickButton() {
        inputData.clicked = true;
    }

    void GetInput() {
        Vector3 atti = AttitudeSensor.current.attitude.value.eulerAngles;
        Debug.Log(StepCounter.current.stepCounter.value);
        inputData.mouse_pos = GetMousePositionFromSensors(atti);
        inputData.mouse_rotation = GetMouseRotationFromSensors(atti);
    }

    void SendInputData(InputData input) {
        // TO DO

        // VV Temp VV
        RectTransform trans = GetComponent<RectTransform>();
        trans.anchoredPosition = new Vector3((canvas.sizeDelta.x / 2f) * input.mouse_pos.x, (canvas.sizeDelta.y / 2f) * input.mouse_pos.y, transform.position.z);
        trans.rotation = Quaternion.Euler(0, 0, input.mouse_rotation);
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
