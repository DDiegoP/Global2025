using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Timer : GameComponent
{
    private double _startTime = 0.0;

    private double _elapsedTime;

    [SerializeField]
    private UnityEvent _callback;

    private bool _finished;

    [SerializeField]
    private TextMeshProUGUI _text;


    void Start()
    {
        _finished = false;
        _elapsedTime = _startTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_finished)
        {
            _elapsedTime += Time.deltaTime;

            if (_text != null) _text.text = Math.Round(_elapsedTime, 2).ToString();

            
        }
    }

    public void StopTimer()
    {
        _elapsedTime = _startTime;
        _callback.Invoke();
        _finished = true;
        
    }
}
