using UnityEngine;

public class ScoreComponent : GameComponent
{
    private double _score;

    private void Awake()
    {
        base.Awake();
    }

    public void changeScore(double factor) {
        _score += factor;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _score = 0;
    }
}
