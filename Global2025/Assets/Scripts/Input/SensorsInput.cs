using UnityEngine;
using UnityEngine.InputSystem;

using Gyroscope = UnityEngine.InputSystem.Gyroscope;

public class SensorsInput : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InputSystem.EnableDevice(Gyroscope.current);
    }

    private void OnDestroy() {
        InputSystem.DisableDevice(Gyroscope.current);
    }

    // Update is called once per frame
    void Update()
    {
        if (Gyroscope.current.enabled)
            Debug.Log(Gyroscope.current.angularVelocity.value);
    }
}
