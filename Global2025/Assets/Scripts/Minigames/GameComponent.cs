using UnityEngine;

public abstract class GameComponent : MonoBehaviour
{
    [SerializeField]
    protected MinigameManager _manager;
    protected void Awake()
    {
        _manager?.RegisterComponent(this);
    }

    public void SetMinigameManager(MinigameManager manager)
    {
        _manager = manager;
        _manager.RegisterComponent(this);
    }

    private void OnDestroy()
    {
        _manager?.UnRegisterComponent(this);
    }
}
