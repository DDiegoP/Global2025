using UnityEngine;
using UnityEngine.InputSystem;

public class CleanComponent : GameComponent
{
    [SerializeField]
    Texture2D dirtMaskTexture;

    [SerializeField]
    Texture2D copyTexture;

    private int totalClean = 237635;


    private RectTransform _rectTransform;

    private Transform _transform;
    void Awake()
    {
        base.Awake();
        ResetTexture();
    }

    void Start()
    {
        _transform = GetComponent<Transform>();
        _rectTransform = GetComponent<RectTransform>();
    }
    public void CleanObject()
    {
        Physics.Raycast(Camera.main.ScreenPointToRay(new Vector3(Mouse.current.position.x.magnitude, Mouse.current.position.y.magnitude, 0)), out RaycastHit hit);
        Vector2 textCoord = hit.textureCoord;

        int pixelX = (int)textCoord.x * dirtMaskTexture.width;
        int pixelY = (int)textCoord.y * dirtMaskTexture.height;

        if (dirtMaskTexture.GetPixel(pixelX, pixelY) != Color.black)
        {
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
    public int checkDirtyNess()
    {
        int clean = 0;
        for (int i = 0; i < dirtMaskTexture.width; ++i)
        {
            for (int j = 0; j < dirtMaskTexture.height; ++j)
            {
                if (dirtMaskTexture.GetPixel(i, j) == Color.black) clean++;
            }

        }
        clean *= 100;
        clean /= totalClean;
        
        return clean;
    }

   
    
}
