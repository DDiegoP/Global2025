using UnityEngine;

public class InstanciateObject : GameComponent
{

    private float elapsedTime = 0f;
    private float instanceTime = 0.25f;

    [SerializeField]
    private GameObject m_Prefab;

    [SerializeField]
    private Transform intanciatePoint;

    private void Awake()
    {
        base.Awake();
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= instanceTime)
        {
            GameObject obj = Instantiate(m_Prefab, intanciatePoint.position, Quaternion.identity);
            obj.transform.SetParent(intanciatePoint);
            obj.GetComponent<BubbleAnimation>().SetMinigameManager(_manager);
            elapsedTime = 0;
        }
    }
}

  
