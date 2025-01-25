using UnityEngine;

public class ScoreComponent : GameComponent
{
    private double _score;

    public void changeScore(double factor) {
        _score += factor;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _score = 0;
    }
}
