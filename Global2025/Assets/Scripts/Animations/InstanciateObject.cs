using UnityEngine;

public class InstanciateObject : MonoBehaviour
{

    private float elapsedTime = 0f;
    private float instanceTime = 0.5f;

    [SerializeField]
    private GameObject m_Prefab;

    [SerializeField]
    private Transform intanciatePoint;

    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= instanceTime)
        {
            Instantiate(m_Prefab, intanciatePoint.position, Quaternion.identity);
            elapsedTime = 0;
        }
    }
}

  
