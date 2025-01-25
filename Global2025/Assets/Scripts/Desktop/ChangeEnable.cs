using UnityEngine;

public class ChangeEnable : MonoBehaviour
{
    [SerializeField]
    private GameObject _gameObject;
    
    public void Change()
    {
        _gameObject.SetActive(!_gameObject.activeSelf);
    }
}
