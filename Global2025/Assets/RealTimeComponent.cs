using System;
using TMPro;
using UnityEngine;

public class RealTimeComponent : MonoBehaviour
{
    private TextMeshProUGUI _text;
    void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        _text.text = DateTime.Now.ToString("HH:mm");
    }
}
