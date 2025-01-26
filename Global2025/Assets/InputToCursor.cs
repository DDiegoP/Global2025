using UnityEngine;

public class InputToCursor : MonoBehaviour
{
    [SerializeField]
    RectTransform _canvas = null;
    RectTransform _tr;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _tr = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        InputData data = InputServerManager.Instance.GetInputData();
        _tr.anchoredPosition = new Vector2(data.mouse_pos_x * _canvas.sizeDelta.x / 2, data.mouse_pos_y * _canvas.sizeDelta.y / 2);
    }
}
