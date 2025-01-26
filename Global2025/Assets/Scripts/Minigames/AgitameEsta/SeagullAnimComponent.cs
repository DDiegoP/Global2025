using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;


public class SeagullAnimComponent : GameComponent
{
    [SerializeField]
    private GameObject endPos;
    private RectTransform rectTrans;
    private RectTransform endTrans;
    [SerializeField]
    private GameObject midPos;
    private RectTransform midTrans;
    private CanvasRenderer canvasRenderer;
    private Rigidbody2D rigidBody;
    [SerializeField]
    private float waitingTime;
    private float endTime; //?
    private bool isMoving;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rectTrans = GetComponent<RectTransform>();
        endTrans = endPos.GetComponent<RectTransform>();
        midTrans = midPos.GetComponent<RectTransform>();
        isMoving = false;
    }

    public void SeagullSusto()
    {
        isMoving = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            var step = 200 * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, endTrans.transform.position, step);
        }
        else
        {
            var step = 50 * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, midTrans.transform.position, step);
        }
    }

}
