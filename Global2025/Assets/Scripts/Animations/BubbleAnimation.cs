using UnityEngine;

public class BubbleAnimation : GameComponent
{
    [SerializeField]
    public float floatSpeed = 1.5f;
    public float sideSpeed = 0.5f;
    public float sideRange = 0.5f;
    public bool randomScale = true;

    [SerializeField]
    public float lifetime = 5f;
    public float shrinkDuration = 2f;

    private float elapsedTime = 0f;
    private Vector3 initialScale;
    private float initialX;
    private int direction;

    [SerializeField]
    private float minScaleFactor = 0.5f;  // El factor mínimo de la escala
    [SerializeField]
    private float maxScaleFactor = 1.0f;  // El factor máximo de la escala (por defecto 1)

    private void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        initialScale = transform.localScale;
        initialX = transform.position.x;

        
        if (randomScale)
        {
            // Randomizamos la escala en el rango entre el mínimo y el valor original (máximo)
            float randomScaleFactor = Random.Range(minScaleFactor, maxScaleFactor);
            transform.localScale = initialScale * randomScaleFactor;
        }
        direction = Random.Range(0, 2) == 0 ? -1 : 1;
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;

        float newX = initialX + Mathf.Sin(elapsedTime * sideSpeed) * sideRange * direction;

        float newY = transform.position.y + (floatSpeed * Time.deltaTime);

        transform.position = new Vector3(newX, newY, transform.position.z);

        if (elapsedTime >= lifetime - shrinkDuration)
        {
            float shrinkFactor = 1 - ((elapsedTime - (lifetime - shrinkDuration)) / shrinkDuration);
            transform.localScale = initialScale * Mathf.Max(shrinkFactor, 0f);
        }

        if (elapsedTime >= lifetime)
        {
            Destroy(gameObject);
        }
    }
}

