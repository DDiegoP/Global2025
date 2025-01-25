using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class TimerComponent : MonoBehaviour {
    [SerializeField]
    private double _startTime = 30.0;

    [SerializeField]
    private double _finishTime = 0.0;

    private bool _decreasing;
    private double _elapsedTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _decreasing = (_startTime > _finishTime);
        _elapsedTime = _startTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (_decreasing)
            _elapsedTime -= Time.deltaTime;
        else _elapsedTime += Time.deltaTime;

        
        if ((_decreasing && _elapsedTime <= _finishTime) || (!_decreasing && _elapsedTime >= _finishTime)) {
            // SE ACABO EL JUEGO
            _elapsedTime = _startTime;
        }
    }
}
