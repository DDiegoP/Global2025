using UnityEngine;


public class BubbleDrinkComponent : GameComponent
{
    [SerializeField]
    private float lifeTime;
    private CanvasRenderer canvasRenderer;
    private Rigidbody2D rigidBody;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       Destroy(gameObject, lifeTime);
       canvasRenderer = GetComponent<CanvasRenderer>();
       canvasRenderer.SetColor(new Color(Random.Range(0.4f, 0.6f), Random.Range(0.1f, 0.2f), Random.Range(0.3f, 0.6f), 1));
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
       canvasRenderer.SetAlpha(canvasRenderer.GetAlpha()-0.0004f);
    }
    private void OnDisable()
    {
        rigidBody.simulated = false;
    }
    private void OnEnable()
    {
        rigidBody.simulated = true;
    }
   
}
