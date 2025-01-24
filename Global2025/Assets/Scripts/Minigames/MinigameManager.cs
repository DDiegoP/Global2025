using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class MinigameManager : MonoBehaviour
{
    private List<GameComponent> _components = new List<GameComponent>();
 
    public void RegisterComponent(GameComponent gameComponent)
    {
        _components.Add(gameComponent);
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

}
