using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TimerComponent : GameComponent {
    [SerializeField]
    private double _startTime = 30.0;

    [SerializeField]
    private double _finishTime = 0.0;

    private bool _decreasing;
    private double _elapsedTime;

    [SerializeField]
    private UnityEvent _callback;

    private bool _finished;

    [SerializeField]
    private TextMeshProUGUI _text;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _finished = false;
        _decreasing = (_startTime > _finishTime);
        _elapsedTime = _startTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_finished) {
            if (_decreasing)
                _elapsedTime -= Time.deltaTime;
            else _elapsedTime += Time.deltaTime;

            if (_text != null) _text.text = "Time: " + Math.Round(_elapsedTime,2);

            if ((_decreasing && _elapsedTime <= _finishTime) || (!_decreasing && _elapsedTime >= _finishTime))
            {
                StopTimer();
            }
        }
    }

    public void StopRunning()
    {
        _finished = true;
        _elapsedTime = _startTime;
    }

    public void StopTimer()
    {
        _finished = true;
        _elapsedTime = _startTime;
        _callback.Invoke();
    }
}
