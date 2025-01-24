using UnityEngine;
using UnityEngine.UI;

public class OpenWindow : MonoBehaviour
{
    [SerializeField]
    private GameObject _window;

    public void Open()
    {
        if (!_window.activeSelf)
        {
            _window.SetActive(true);
            // Resettear minijuego
        }
    }
}
