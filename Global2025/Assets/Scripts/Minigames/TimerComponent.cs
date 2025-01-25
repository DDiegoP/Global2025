using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TimerComponent : MonoBehaviour {
    [SerializeField]
    private double _startTime = 30.0;

    [SerializeField]
    private double _finishTime = 0.0;

    private bool _decreasing;
    private double _elapsedTime;

    [SerializeField]
    private UnityEvent _callback;

    private bool _finished;

    private bool _refreshText;
    private Text _text;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _finished = false;
        _decreasing = (_startTime > _finishTime);
        _elapsedTime = _startTime;
        _text = gameObject.GetComponent<Text>();
        _refreshText = _text != null;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_finished) {
            if (_decreasing)
                _elapsedTime -= Time.deltaTime;
            else _elapsedTime += Time.deltaTime;

            if (_refreshText)
            {
                _text.text = ((int) _elapsedTime) +  "s";
            }

            if ((_decreasing && _elapsedTime <= _finishTime) || (!_decreasing && _elapsedTime >= _finishTime))
            {
                // SE ACABO EL JUEGO
                _elapsedTime = _startTime;
                _callback.Invoke();
                _finished = true;
            }
        }
    }
}
