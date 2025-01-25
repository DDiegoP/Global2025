using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.InputSystem;

// using this for mouse clicking on Disable UI to add popup for Mobile since there is no Mouse over functionality on touch screen
[RequireComponent(typeof(Button))]
public class UIAddToolTip : MonoBehaviour
{
    public GameObject UI_MouseOverToolTip;

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            bool isPointOVerThis = RaycastUtilities.PointerIsOverUI((new Vector3(Mouse.current.position.ReadValue().x, Mouse.current.position.ReadValue().y, 0.0f)), this.gameObject);

            if (isPointOVerThis)
            {
                Debug.Log("Pointer is OVer disabled UI");
                
            }
        }
    }
}

//added a check to see if  this button was hit with raycast
public static class RaycastUtilities
{
    public static bool PointerIsOverUI(Vector2 screenPos, GameObject GO)
    {
        var hitObject = UIRaycast(ScreenPosToPointerData(screenPos));
        return hitObject != null && hitObject.layer == LayerMask.NameToLayer("UI") && hitObject == GO;
    }

    public static GameObject UIRaycast(PointerEventData pointerData)
    {
        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);
        Debug.Log(results[0].gameObject.name + "Was raycast hit...");
        return results.Count < 1 ? null : results[0].gameObject;
    }

    static PointerEventData ScreenPosToPointerData(Vector2 screenPos)
       => new(EventSystem.current) { position = screenPos };
}