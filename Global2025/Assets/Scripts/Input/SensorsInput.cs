using UnityEngine;
using UnityEngine.InputSystem;

using Gyroscope = UnityEngine.InputSystem.Gyroscope;

public class SensorsInput : MonoBehaviour
{
    [SerializeField]
    float scale_x = 5f, scale_y = 7f, gyro_dz = 0.15f, accel_dz = 0.5f;

    InputData lastInput;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        lastInput = new InputData();
        lastInput.mouse_pos = Vector2.zero;
        lastInput.mouse_rotation = 0f;
        lastInput.clicked = false;

        InputSystem.EnableDevice(Gyroscope.current);
        InputSystem.EnableDevice(Accelerometer.current);
    }

    private void OnDestroy() {
        InputSystem.DisableDevice(Accelerometer.current);
        InputSystem.DisableDevice(Gyroscope.current);
    }

    Vector2 GetMousePositionFromGyro(Vector3 gyro) {
        return lastInput.mouse_pos + new Vector2(-scale_x * (gyro.z) * Time.deltaTime, scale_y * (gyro.x) * Time.deltaTime);
    }

    InputData GetInput() {
        InputData input = new InputData();
        
        if (!Gyroscope.current.enabled)
            return input;

        // Z -> X && X -> Y
        Vector3 gyro = Gyroscope.current.angularVelocity.value;

        input.mouse_pos = GetMousePositionFromGyro(gyro);
        input.mouse_rotation = -gyro.y;
        input.clicked = false;
        
        return input;
    }

    void SendInputData(InputData input) {
        // TO DO

        // VV Temp VV
        transform.position = new Vector3(input.mouse_pos.x, input.mouse_pos.y, transform.position.z);
        transform.Rotate(0, 0, input.mouse_rotation);
    }

    // Update is called once per frame
    void Update() {
        InputData currentInput = GetInput();
        SendInputData(currentInput);
        lastInput = currentInput;
    }
}
