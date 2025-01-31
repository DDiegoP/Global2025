using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class WindowManager : MonoBehaviour
{
    private List<WindowController> _controllers = new List<WindowController>();


    public void AddController(WindowController controller)
    {
        _controllers.Add(controller);
        updateOrder();
    }

    public void UpdateController(WindowController controller)
    {
        _controllers.Remove(controller);
        _controllers.Add(controller);
        updateOrder(); // se puede cambiar para recorrer solo una vez
    }

    public void RemoveController(WindowController controller)
    {
        _controllers.Remove(controller);
        updateOrder();
    }

    private void updateOrder()
    {
        int i = 1;
        foreach (WindowController controller in _controllers)
        {
            controller.SetOrder(i, i == _controllers.Count);
            ++i;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
