using UnityEngine;
using UnityEngine.InputSystem;

public class CleanComponent : GameComponent
{
    [SerializeField]
    Texture2D dirtMaskTexture;

    private RectTransform _rectTransform;
    void Awake()
    {
        base.Awake();
    }

    public void CleanObject()
    {
        Debug.Log("hola");
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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Debug.Log("hola");

            Physics.Raycast((new Vector3(Mouse.current.position.ReadValue().x, Mouse.current.position.ReadValue().y, 0.0f)), Camera.main.transform.forward, out RaycastHit hit);
            Vector2Int localPos = new Vector2Int();
            localPos.x = (int)(_rectTransform.position.x - hit.point.x);
            localPos.y = (int)(_rectTransform.position.y - hit.point.y);

            int pixelX = localPos.x /  dirtMaskTexture.width;
            int pixelY = localPos.x / dirtMaskTexture.width;

            Debug.Log(pixelX + " " + pixelY);
            Debug.Log(dirtMaskTexture.GetPixel(pixelX, pixelY));
            if (dirtMaskTexture.GetPixel(pixelX, pixelY) != Color.black)
            {
                Debug.Log("te pinto negro");
                dirtMaskTexture.SetPixel(pixelX, pixelY, Color.black);
            }
        }
    }
}
