using UnityEngine;
using UnityEngine.InputSystem;

public class CleanComponent : MonoBehaviour
{
    [SerializeField]
    Texture2D dirtMaskTexture;

    public void CleanObject()
    {
        Debug.Log("hola");
        Physics.Raycast(Camera.main.ScreenPointToRay(new Vector3(Mouse.current.position.x.magnitude, Mouse.current.position.y.magnitude, 0)), out RaycastHit hit);
        Vector2 textCoord = hit.textureCoord;

        int pixelX = (int)textCoord.x * dirtMaskTexture.width;
        int pixelY = (int)textCoord.y * dirtMaskTexture.height;

        Vector2Int paintPixelPos = new Vector2Int(pixelX, pixelY);

        if (dirtMaskTexture.GetPixel(pixelX, pixelY) != Color.black)
        {
            Debug.Log("te pinto negro");
            dirtMaskTexture.SetPixel(pixelX, pixelY, Color.black);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
