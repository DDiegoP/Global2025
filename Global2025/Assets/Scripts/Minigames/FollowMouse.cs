using UnityEngine;
using UnityEngine.InputSystem;

public class FollowMouse : GameComponent
{

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = new Vector3(Mouse.current.position.x.value, Mouse.current.position.y.value, transform.position.z);
        transform.SetPositionAndRotation(pos, Quaternion.identity);
    }
}
