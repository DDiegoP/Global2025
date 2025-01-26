using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class FadeOut : GameComponent
{
    private float _elapsedTime = 0;
    private bool _started = false;
    private RawImage _image;

    [SerializeField]
    private UnityEvent afterFadeOut;

    [SerializeField]
    private float _fadeOutTime;


    private void Awake()
    {
        base.Awake();
    }

    public void StartFadeOut()
    {
        _started = true;
        _elapsedTime = 0;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _image = GetComponent<RawImage>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_started)
        {
            _elapsedTime += Time.deltaTime;
            _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, _elapsedTime / _fadeOutTime);
            if (_elapsedTime >= _fadeOutTime)
            {
                _started = false;
                afterFadeOut.Invoke();
            }
        }
    }
}
