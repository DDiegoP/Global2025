using UnityEngine;

public class Shrink : GameComponent
{
    [SerializeField]
    public float lifetime = 2f;
    public float shrinkDuration = 2f;

    private Vector3 initialScale;

    private float elapsedTime = 0f;


    private void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        initialScale = transform.localScale;
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;


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
