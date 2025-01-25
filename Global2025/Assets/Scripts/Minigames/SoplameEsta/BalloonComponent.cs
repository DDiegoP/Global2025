using UnityEngine;

public class BalloonComponent : GameComponent
{
    private double _air = 0.0;

    private double _scale = 1.0;

    private bool _blowing = false;

    [SerializeField]
    private float maxScale = 4;

    private void Pop()
    {

    }

    private void ApplyScore()
    {

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Se pilla el input de soplido y se aumenta el "aire" del globo
        // if (!blowing) VER SI EMPIEZA A SOPLAR

        if (_blowing)
        {
            // Aumentamos el aire del globo (y con ello la escala)
            _air += 0.05;
            _scale += _air;
            if (_air)
            // Si deja de soplar ->
            
        }
    }
}
