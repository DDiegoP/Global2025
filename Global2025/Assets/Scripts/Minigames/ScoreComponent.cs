using TMPro;
using UnityEngine;

public class ScoreComponent : GameComponent
{
    private double _score;
    [SerializeField]
    private TextMeshProUGUI _text;
    public void changeScore(double factor) {
        _score += factor;
        if(_text != null ) _text.text = "Score: " + _score;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _score = 0;
        if (_text != null) _text.text = "Score: " + _score;
    }
}
