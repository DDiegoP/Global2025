using TMPro;
using UnityEngine;

public class HighScoreComponent : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _balloonScore, _fishScore, _soapScore, _sodaScore;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _balloonScore.text = PlayerPrefs.GetInt("BalloonHighScore", 0).ToString();
        _fishScore.text = PlayerPrefs.GetInt("FishHighScore", 0).ToString();
        _soapScore.text = PlayerPrefs.GetInt("SoapHighScore", 0).ToString();
        _sodaScore.text = PlayerPrefs.GetInt("SodaHighScore", 0).ToString();
    }
}
