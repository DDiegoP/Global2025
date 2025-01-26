using Unity.VisualScripting;
using UnityEngine;


public class BubbleDrinkComponent : GameComponent
{
    [SerializeField]
    private float lifeTime;
    private CanvasRenderer canvasRenderer;
    private Rigidbody2D rigidBody;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        rigidBody = this.GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        Destroy(gameObject, lifeTime);
        canvasRenderer = this.GetComponent<CanvasRenderer>();
        canvasRenderer.SetColor(new Color(Random.Range(0.4f, 0.6f), Random.Range(0.1f, 0.2f), Random.Range(0.3f, 0.6f), 1));
    }

    // Update is called once per frame
    void Update()
    {
        canvasRenderer.SetAlpha(canvasRenderer.GetAlpha() - 0.0004f);
    }

    private void OnEnable()
    {
        rigidBody.simulated = true;
    }

    private void OnDisable()
    {
        rigidBody.simulated = false;
    }
}
