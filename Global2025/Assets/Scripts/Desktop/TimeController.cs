using System;
using TMPro;
using UnityEngine;


public class TimeController : MonoBehaviour
{
    private TextMeshProUGUI _text;

    private void Start()
    {
        _text = this.gameObject.GetComponent<TextMeshProUGUI>();
    }
    void Update()
    {

        if (_text != null) _text.text = DateTime.Now.ToString();
    }
}
