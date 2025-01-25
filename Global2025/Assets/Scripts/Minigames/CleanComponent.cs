using UnityEngine;
using UnityEngine.InputSystem;

public class CleanComponent : GameComponent
{
    [SerializeField]
    Texture2D dirtMaskTexture;

    [SerializeField]
    Texture2D copyTexture;

    private RectTransform _rectTransform;
    void Awake()
    {
        base.Awake();
        ResetTexture();
    }

    public void CleanObject()
    {
        Physics.Raycast(Camera.main.ScreenPointToRay(new Vector3(Mouse.current.position.x.magnitude, Mouse.current.position.y.magnitude, 0)), out RaycastHit hit);
        Vector2 textCoord = hit.textureCoord;

        int pixelX = (int)textCoord.x * dirtMaskTexture.width;
        int pixelY = (int)textCoord.y * dirtMaskTexture.height;

        if (dirtMaskTexture.GetPixel(pixelX, pixelY) != Color.black)
        {
            Debug.Log("te pinto negro");
            dirtMaskTexture.SetPixel(pixelX, pixelY, Color.black);
        }
    }

    public void CleanRectangle(Vector2 minCorner, Vector2 maxCorner)
    {
        for(int i = (int)minCorner.x; i < maxCorner.x; ++i)
        {
            for (int j = (int)minCorner.y; j < maxCorner.y; ++j)
            {
                dirtMaskTexture.SetPixel(i,j, Color.black);
            }

        }

        dirtMaskTexture.Apply();
    }

    public void ResetTexture()
    {
        for (int i = 0; i < copyTexture.width; ++i)
        {
            for (int j = 0; j < copyTexture.height; ++j)
            {
                Color c = copyTexture.GetPixel(i, j);
                dirtMaskTexture.SetPixel(i, j, c);
            }
        }
        dirtMaskTexture.Apply();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

}
