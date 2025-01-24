using UnityEngine;

public abstract class GameComponent : MonoBehaviour
{
    [SerializeField]
    private MinigameManager _manager;
    protected void Awake()
    {
        _manager.RegisterComponent(this);
    }
}
