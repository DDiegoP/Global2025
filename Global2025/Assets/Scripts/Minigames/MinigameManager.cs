using FMODUnity;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinigameManager : MonoBehaviour
{
    private List<GameComponent> _components = new List<GameComponent>();
    private WindowController _controller;

    [SerializeField]
    private bool _isGame;

    [SerializeField]
    private RawImage _background;

    [SerializeField]
    private StudioEventEmitter _musicEmitter = null;

    public int trackEvent = 1;

    public void SetColor(Color c)
    {
        if(_background) _background.color = new Color(c.r, c.g, c.b, _isGame ? 0 : 1);
    }

    public void Awake()
    {
        _controller = GetComponentInParent<WindowController>();
        _musicEmitter = GameObject.FindGameObjectWithTag("MainCanvas").GetComponent<StudioEventEmitter>();
    }

    private void Start()
    {
        _musicEmitter?.SetParameter("Track" + trackEvent.ToString(), 1);
    }

    public void RegisterComponent(GameComponent gameComponent)
    {
        _components.Add(gameComponent);
    }

    public void UnRegisterComponent(GameComponent gameComponent)
    {
        _components.Remove(gameComponent);
    }

    private void OnDisable()
    {
        foreach (GameComponent component in _components)
        {
            component.enabled = false;
        }
    }

    private void OnEnable()
    {
        foreach (GameComponent component in _components) 
        {
            component.enabled = true;
        }
    }

    public void ChangeScene()
    {
        _controller.InstantiateNextScene(!_isGame);
    }

    private void OnDestroy()
    {
        _musicEmitter?.SetParameter("Track" + trackEvent.ToString(), 0);
    }
}
